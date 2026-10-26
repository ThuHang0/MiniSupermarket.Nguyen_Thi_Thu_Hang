using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = "Admin,Cashier")]
public class OrdersController : ControllerBase
{
    private readonly string _cs;
    private readonly ILogger<OrdersController> _logger;
    public OrdersController(IConfiguration configuration, ILogger<OrdersController> logger)
    {
        _cs = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu DefaultConnection.");
        _logger = logger;
    }

    private static SqlCommand Cmd(SqlConnection cn, SqlTransaction tx, string sql,
        params (string Name, object? Value)[] args)
    {
        var cmd = new SqlCommand(sql, cn, tx);
        foreach (var arg in args) cmd.Parameters.AddWithValue(arg.Name, arg.Value ?? DBNull.Value);
        return cmd;
    }

    private void Rollback(SqlTransaction tx)
    {
        try { tx.Rollback(); }
        catch (Exception ex) { _logger.LogError(ex, "Rollback thất bại."); }
    }

    // Đường cũ để FormPOS tiếp tục hoạt động và đường theo đề.
    [HttpPost("checkout")]
    [HttpPost("/api/checkout")]
    public IActionResult Checkout([FromBody] CheckoutRequest request)
    {
        if (request.RequestId == Guid.Empty)
            return BadRequest(new { message = "RequestId phải là GUID mới cho mỗi lần mua hàng." });
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int cashierId))
            return Unauthorized();

        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var tx = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            using (var cmd = Cmd(cn, tx,
                "SELECT COUNT(*) FROM dbo.Users WHERE Id=@Id AND IsActive=1 AND Role IN ('Admin','Cashier')",
                ("@Id", cashierId)))
                if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
                    throw new InvalidOperationException("Tài khoản không được phép bán hàng.");

            // Giữ cơ chế chống thanh toán trùng của code cũ.
            using (var cmd = Cmd(cn, tx,
                "SELECT Id, CashierId, TotalAmount, CashReceived FROM dbo.Orders WITH (UPDLOCK,HOLDLOCK) WHERE RequestId=@RequestId",
                ("@RequestId", request.RequestId)))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (Convert.ToInt32(reader["CashierId"]) != cashierId)
                        throw new InvalidOperationException("RequestId đã được sử dụng.");
                    int id = Convert.ToInt32(reader["Id"]);
                    decimal amount = Convert.ToDecimal(reader["TotalAmount"]);
                    decimal change = Convert.ToDecimal(reader["CashReceived"]) - amount;
                    reader.Close();
                    tx.Commit();
                    return Ok(new { orderId = id, totalAmount = amount, change });
                }
            }

            int? customerId = request.CustomerId;
            string phone = request.CustomerPhone.Trim();
            if (phone.Length > 0)
            {
                using var cmd = Cmd(cn, tx,
                    "SELECT CustomerId FROM dbo.Customers WHERE PhoneNumber=@Phone", ("@Phone", phone));
                object? value = cmd.ExecuteScalar();
                if (value == null) throw new InvalidOperationException("Số điện thoại khách hàng chưa đăng ký.");
                int found = Convert.ToInt32(value);
                if (customerId.HasValue && customerId.Value != found)
                    throw new InvalidOperationException("CustomerId và số điện thoại không cùng khách hàng.");
                customerId = found;
            }
            if (customerId.HasValue)
            {
                using var cmd = Cmd(cn, tx,
                    "SELECT COUNT(*) FROM dbo.Customers WHERE CustomerId=@Id", ("@Id", customerId));
                if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
                    throw new InvalidOperationException("Khách hàng không tồn tại.");
            }

            // INSERT trước khi xử lý hàng để bài kiểm thử vượt tồn chứng minh rollback.
            int orderId;
            using (var cmd = Cmd(cn, tx, @"
                INSERT INTO dbo.Orders
                    (RequestId,OrderDate,CashierId,CustomerId,TotalAmount,CashReceived,RewardPoints)
                OUTPUT INSERTED.Id
                VALUES (@RequestId,@Date,@Cashier,@Customer,0,@Cash,0)",
                ("@RequestId", request.RequestId),
                ("@Date", DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime),
                ("@Cashier", cashierId), ("@Customer", customerId), ("@Cash", request.CashReceived)))
                orderId = Convert.ToInt32(cmd.ExecuteScalar());

            decimal total = 0;
            int totalItems = 0;
            foreach (var group in request.Items.GroupBy(x => x.ProductId).OrderBy(x => x.Key))
            {
                long quantity = group.Sum(x => (long)x.Quantity);
                if (quantity <= 0 || quantity > 1000000)
                    throw new InvalidOperationException("Số lượng không hợp lệ.");
                string name;
                decimal price;
                int stock;
                using (var cmd = Cmd(cn, tx, @"
                    SELECT ProductName,Price,StockQuantity FROM dbo.Products WITH (UPDLOCK,HOLDLOCK)
                    WHERE ProductId=@Id", ("@Id", group.Key)))
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException($"Sản phẩm {group.Key} không tồn tại.");
                    name = Convert.ToString(reader["ProductName"]) ?? "";
                    price = Convert.ToDecimal(reader["Price"]);
                    stock = Convert.ToInt32(reader["StockQuantity"]);
                }
                if (price <= 0)
                    throw new InvalidOperationException(
                        "Giá sản phẩm phải lớn hơn 0.");
                if (stock < quantity) throw new InvalidOperationException($"{name} không đủ tồn kho: còn {stock}, mua {quantity}.");
                total += price * quantity;
                totalItems += (int)quantity;
                if (total > 9999999999999999.99m) throw new InvalidOperationException("Tổng tiền vượt giới hạn.");

                using (var cmd = Cmd(cn, tx, @"
                    INSERT INTO dbo.OrderDetails (OrderId,ProductId,ProductName,UnitPrice,Quantity)
                    VALUES (@Order,@Product,@Name,@Price,@Qty)",
                    ("@Order", orderId), ("@Product", group.Key), ("@Name", name),
                    ("@Price", price), ("@Qty", (int)quantity))) cmd.ExecuteNonQuery();
                using (var cmd = Cmd(cn, tx, @"
                    UPDATE dbo.Products SET StockQuantity=StockQuantity-@Qty
                    WHERE ProductId=@Id AND StockQuantity>=@Qty",
                    ("@Qty", (int)quantity), ("@Id", group.Key)))
                    if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Tồn kho đã thay đổi.");
            }
            if (request.CashReceived < total) throw new InvalidOperationException("Tiền khách đưa chưa đủ.");
            decimal pointsDecimal = customerId.HasValue ? decimal.Floor(total / 10000m) : 0;
            if (pointsDecimal > int.MaxValue) throw new InvalidOperationException("Điểm thưởng vượt giới hạn.");
            int points = (int)pointsDecimal;
            using (var cmd = Cmd(cn, tx, @"
                UPDATE dbo.Orders SET TotalAmount=@Total,RewardPoints=@Points WHERE Id=@Id",
                ("@Total", total), ("@Points", points), ("@Id", orderId))) cmd.ExecuteNonQuery();
            if (customerId.HasValue && points > 0)
            {
                using var cmd = Cmd(cn, tx, @"
                    UPDATE dbo.Customers SET RewardPoints=RewardPoints+@Points WHERE CustomerId=@Id",
                    ("@Points", points), ("@Id", customerId));
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không cập nhật được điểm khách hàng.");
            }
            tx.Commit();
            return Ok(new
            {
                success = true,
                orderId,
                totalAmount = total,
                finalAmount = total,
                totalItems,
                change = request.CashReceived - total,
                rewardPointsAdded = points,
                message = "Thanh toán thành công."
            });
        }
        catch (InvalidOperationException ex)
        {
            Rollback(tx);
            return BadRequest(new { success = false, message = "Thanh toán bị hủy, giao dịch đã Rollback.", errorDetail = ex.Message });
        }
        catch (SqlException ex)
        {
            Rollback(tx);
            _logger.LogError(ex, "Lỗi SQL khi thanh toán.");
            return Conflict(new { success = false, message = "Không thể lưu giao dịch. Kiểm tra log API và thử lại với cùng RequestId." });
        }
        catch (Exception ex)
        {
            Rollback(tx);
            _logger.LogError(ex, "Lỗi thanh toán.");
            return StatusCode(500, new { success = false, message = "Không thể hoàn tất thanh toán." });
        }
    }

    [HttpDelete("cancel/{orderId:int}")]
    [HttpDelete("/api/checkout/cancel/{orderId:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult CancelOrder(int orderId)
    {
        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var tx = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            int? customerId;
            int points;
            using (var cmd = Cmd(cn, tx,
                "SELECT CustomerId,RewardPoints FROM dbo.Orders WITH (UPDLOCK,HOLDLOCK) WHERE Id=@Id", ("@Id", orderId)))
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    reader.Close();
                    tx.Rollback();
                    return NotFound(new { message = "Không tìm thấy hóa đơn." });
                }
                customerId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                points = reader.GetInt32(1);
            }
            var items = new List<(int Id, int Qty)>();
            using (var cmd = Cmd(cn, tx,
                "SELECT ProductId,SUM(Quantity) FROM dbo.OrderDetails WHERE OrderId=@Id GROUP BY ProductId ORDER BY ProductId", ("@Id", orderId)))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read()) items.Add((reader.GetInt32(0), reader.GetInt32(1)));
            foreach (var item in items)
            {
                using var cmd = Cmd(cn, tx,
                    "UPDATE dbo.Products SET StockQuantity=StockQuantity+@Qty WHERE ProductId=@Id",
                    ("@Qty", item.Qty), ("@Id", item.Id));
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không hoàn được tồn kho.");
            }
            if (customerId.HasValue && points > 0)
            {
                using var cmd = Cmd(cn, tx, @"
                    UPDATE dbo.Customers SET RewardPoints=CASE WHEN RewardPoints>=@Points
                        THEN RewardPoints-@Points ELSE 0 END WHERE CustomerId=@Id",
                    ("@Points", points), ("@Id", customerId));
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không tìm thấy khách hàng.");
            }
            using (var cmd = Cmd(cn, tx, "DELETE FROM dbo.OrderDetails WHERE OrderId=@Id", ("@Id", orderId))) cmd.ExecuteNonQuery();
            using (var cmd = Cmd(cn, tx, "DELETE FROM dbo.Orders WHERE Id=@Id", ("@Id", orderId)))
                if (cmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không xóa được hóa đơn.");
            tx.Commit();
            return Ok(new { success = true, message = "Đã hủy hóa đơn, hoàn kho và thu hồi điểm.", orderId });
        }
        catch (Exception ex)
        {
            Rollback(tx);
            _logger.LogError(ex, "Lỗi hủy hóa đơn {OrderId}", orderId);
            return BadRequest(new { success = false, message = "Hủy hóa đơn thất bại, giao dịch đã Rollback." });
        }
    }
}

public class CheckoutRequest
{
    public Guid RequestId { get; set; }
    [Range(1, int.MaxValue)] public int? CustomerId { get; set; }
    [StringLength(15)] public string CustomerPhone { get; set; } = "";
    [Range(typeof(decimal), "0", "9999999999999999.99")] public decimal CashReceived { get; set; }
    [Required, MinLength(1), MaxLength(200)] public List<CheckoutItemRequest> Items { get; set; } = new();
}
public class CheckoutItemRequest
{
    [Range(1, int.MaxValue)] public int ProductId { get; set; }
    [Range(1, 1000000)] public int Quantity { get; set; }
}
