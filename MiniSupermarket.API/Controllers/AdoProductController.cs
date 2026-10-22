using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/ado-products")]
[Authorize(Roles = "Admin,Cashier,Warehouse")]
public class AdoProductController : ControllerBase
{
    private readonly string _cs;
    public AdoProductController(IConfiguration configuration) =>
        _cs = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu DefaultConnection.");

    private const string Columns = "ProductId, Barcode, ProductName, Price, StockQuantity";
    private static AdoProductDto Map(SqlDataReader r) => new()
    {
        ProductId = Convert.ToInt32(r["ProductId"]),
        Barcode = Convert.ToString(r["Barcode"]) ?? "",
        ProductName = Convert.ToString(r["ProductName"]) ?? "",
        Price = Convert.ToDecimal(r["Price"]),
        StockQuantity = Convert.ToInt32(r["StockQuantity"])
    };

    // Tiết 1 - bài 1: một ô dữ liệu.
    [HttpGet("count")]
    public IActionResult Count()
    {
        using var cn = new SqlConnection(_cs);
        using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Products", cn);
        cn.Open();
        return Ok(new { totalProducts = Convert.ToInt32(cmd.ExecuteScalar()) });
    }

    // Tiết 1 - bài 2: UPDATE có tham số.
    [HttpPut("{id:int}/stock")]
    [Authorize(Roles = "Admin,Warehouse")]
    public IActionResult UpdateStock(int id, [FromBody] AdoStockRequest request)
    {
        if (id <= 0 || request.StockQuantity < 0)
            return BadRequest(new { message = "ID hoặc tồn kho không hợp lệ." });
        using var cn = new SqlConnection(_cs);
        using var cmd = new SqlCommand(
            "UPDATE dbo.Products SET StockQuantity=@Stock WHERE ProductId=@Id", cn);
        cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = request.StockQuantity;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        cn.Open();
        int affected = cmd.ExecuteNonQuery();
        if (affected == 0) return NotFound(new { message = "Không tìm thấy sản phẩm." });
        return Ok(new { rowsAffected = affected, request.StockQuantity });
    }

    // Tiết 1 - bài 3: luồng đọc danh sách.
    [HttpGet]
    public IActionResult GetAll()
    {
        using var cn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"SELECT {Columns} FROM dbo.Products ORDER BY ProductId", cn);
        cn.Open();
        using var reader = cmd.ExecuteReader();
        var data = new List<AdoProductDto>();
        while (reader.Read()) data.Add(Map(reader));
        return Ok(data);
    }

    [HttpGet("barcode/{barcode}")]
    public IActionResult Barcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode) || barcode.Length > 50)
            return BadRequest(new { message = "Mã vạch không hợp lệ." });
        using var cn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"SELECT {Columns} FROM dbo.Products WHERE Barcode=@Barcode", cn);
        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = barcode.Trim();
        cn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read()) return Ok(Map(reader));
        return NotFound(new { message = "Không tìm thấy mã vạch này." });
    }

    [HttpGet("disconnected-cache")]
    public IActionResult DisconnectedCache()
    {
        using var table = new DataTable();
        using (var cn = new SqlConnection(_cs))
        using (var cmd = new SqlCommand($"SELECT {Columns} FROM dbo.Products ORDER BY ProductId", cn))
        using (var adapter = new SqlDataAdapter(cmd))
        {
            // Fill tự mở/đóng kết nối vì kết nối ban đầu đang đóng.
            adapter.Fill(table);
        }
        var data = table.AsEnumerable().Select(r => new AdoProductDto
        {
            ProductId = Convert.ToInt32(r["ProductId"]),
            Barcode = Convert.ToString(r["Barcode"]) ?? "",
            ProductName = Convert.ToString(r["ProductName"]) ?? "",
            Price = Convert.ToDecimal(r["Price"]),
            StockQuantity = Convert.ToInt32(r["StockQuantity"])
        }).ToList();
        return Ok(new { totalLoaded = data.Count, cachedAt = DateTime.Now, data });
    }

    [HttpGet("filter-stock")]
    public IActionResult FilterStock([FromQuery] int minStock = 50)
    {
        if (minStock < 0) return BadRequest(new { message = "Ngưỡng tồn kho phải không âm." });
        using var cn = new SqlConnection(_cs);
        using var cmd = new SqlCommand($"SELECT {Columns} FROM dbo.Products WHERE StockQuantity<=@MinStock ORDER BY ProductId", cn);
        cmd.Parameters.Add("@MinStock", SqlDbType.Int).Value = minStock;
        cn.Open();
        using var reader = cmd.ExecuteReader();
        var data = new List<AdoProductDto>();
        while (reader.Read()) data.Add(Map(reader));
        return Ok(data);
    }

    [HttpGet("count-by-category")]
    public IActionResult CountByCategory()
    {
        using var table = new DataTable();
        using (var cn = new SqlConnection(_cs))
        using (var adapter = new SqlDataAdapter(
            "SELECT CategoryId, COUNT(*) AS Total FROM dbo.Products GROUP BY CategoryId ORDER BY CategoryId", cn))
            adapter.Fill(table);
        return Ok(table.AsEnumerable().Select(r => new
        {
            categoryId = Convert.ToInt32(r["CategoryId"]),
            total = Convert.ToInt32(r["Total"])
        }).ToList());
    }
}

public class AdoProductDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = "";
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
public class AdoStockRequest { public int StockQuantity { get; set; } }
