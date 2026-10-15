using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // POST /api/orders/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            [FromBody] CheckoutRequest request)
        {
            if (request.RequestId == Guid.Empty)
            {
                return BadRequest(new
                {
                    message = "Mã yêu cầu thanh toán không hợp lệ."
                });
            }

            if (!int.TryParse(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                out int cashierId))
            {
                return Unauthorized();
            }

            var cashier = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Id == cashierId && u.IsActive);

            if (cashier == null ||
                (cashier.Role != "Admin" &&
                 cashier.Role != "Cashier"))
            {
                return StatusCode(403, new
                {
                    message = "Tài khoản không được phép bán hàng."
                });
            }

            // Gửi lại cùng RequestId không tạo thêm hóa đơn.
            var existing = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o =>
                    o.RequestId == request.RequestId);

            if (existing != null)
            {
                if (existing.CashierId != cashierId)
                {
                    return Conflict(new
                    {
                        message = "Mã yêu cầu đã được sử dụng."
                    });
                }

                return Ok(new
                {
                    orderId = existing.Id,
                    existing.TotalAmount,
                    change = existing.CashReceived -
                        existing.TotalAmount
                });
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable);

            try
            {
                // Kiểm tra lại bên trong giao dịch.
                existing = await _context.Orders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o =>
                        o.RequestId == request.RequestId);

                if (existing != null)
                {
                    if (existing.CashierId != cashierId)
                    {
                        return Conflict(new
                        {
                            message = "Mã yêu cầu đã được sử dụng."
                        });
                    }

                    return Ok(new
                    {
                        orderId = existing.Id,
                        existing.TotalAmount,
                        change = existing.CashReceived -
                            existing.TotalAmount
                    });
                }

                string phone = request.CustomerPhone.Trim();

                Customer? customer = null;

                // Bỏ trống số điện thoại = khách vãng lai.
                if (phone.Length > 0)
                {
                    customer = await _context.Customers
                        .FirstOrDefaultAsync(c =>
                            c.PhoneNumber == phone);

                    if (customer == null)
                    {
                        return BadRequest(new
                        {
                            message =
                                "Số điện thoại khách hàng chưa đăng ký."
                        });
                    }
                }

                var order = new Order
                {
                    RequestId = request.RequestId,
                    CashierId = cashierId,
                    CustomerId = customer?.CustomerId,

                    // Lưu giờ Việt Nam.
                    OrderDate = DateTimeOffset.UtcNow
                        .ToOffset(TimeSpan.FromHours(7)).DateTime,

                    CashReceived = request.CashReceived
                };

                decimal total = 0;

                var groups = request.Items
                    .GroupBy(i => i.ProductId)
                    .OrderBy(g => g.Key);

                foreach (var group in groups)
                {
                    long quantity =
                        group.Sum(i => (long)i.Quantity);

                    if (quantity <= 0 || quantity > 1000000)
                    {
                        return BadRequest(new
                        {
                            message = "Số lượng sản phẩm không hợp lệ."
                        });
                    }

                    var product = await _context.Products
                        .FirstOrDefaultAsync(p =>
                            p.ProductId == group.Key);

                    if (product == null)
                    {
                        return BadRequest(new
                        {
                            message =
                                $"Sản phẩm {group.Key} không tồn tại."
                        });
                    }

                    if (product.Price < 0)
                    {
                        return BadRequest(new
                        {
                            message = "Giá sản phẩm không hợp lệ."
                        });
                    }

                    if (product.StockQuantity < quantity)
                    {
                        return BadRequest(new
                        {
                            message =
                                $"{product.ProductName} không đủ tồn kho."
                        });
                    }

                    // Lấy giá trong database, không nhận giá từ client.
                    total += product.Price * quantity;

                    if (total > 9999999999999999.99m)
                    {
                        return BadRequest(new
                        {
                            message = "Tổng hóa đơn vượt giới hạn."
                        });
                    }

                    product.StockQuantity -= (int)quantity;

                    order.Details.Add(new OrderDetail
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = (int)quantity
                    });
                }

                if (request.CashReceived < total)
                {
                    return BadRequest(new
                    {
                        message = "Tiền khách đưa chưa đủ."
                    });
                }

                order.TotalAmount = total;

                _context.Orders.Add(order);

                // Lưu hóa đơn, chi tiết và tồn kho.
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new
                {
                    orderId = order.Id,
                    order.TotalAmount,
                    change = order.CashReceived - order.TotalAmount
                });
            }
            catch (Exception ex) when (
                ex is DbUpdateException ||
                ex is Microsoft.Data.SqlClient.SqlException)
            {
                await transaction.RollbackAsync();

                return Conflict(new
                {
                    message = "Dữ liệu bán hàng đang thay đổi. " +
                              "Hãy thử thanh toán lại."
                });
            }
        }
    }

    public class CheckoutRequest
    {
        public Guid RequestId { get; set; }

        [StringLength(15)]
        public string CustomerPhone { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal CashReceived { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public List<CheckoutItemRequest> Items { get; set; } = new();
    }

    public class CheckoutItemRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, 1000000)]
        public int Quantity { get; set; }
    }
}