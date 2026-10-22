using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        private readonly IConfiguration _configuration;

        public ReportsController(SupermarketDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET /api/reports/quick?date=2026-10-15
        [HttpGet("quick")]
        public async Task<IActionResult> GetQuickReport(
            [FromQuery] DateTime date)
        {
            if (date == default ||
                date.Date == DateTime.MaxValue.Date)
            {
                return BadRequest(new
                {
                    message = "Ngày báo cáo không hợp lệ."
                });
            }

            DateTime start = date.Date;
            DateTime end = start.AddDays(1);

            // OrderDate được lưu theo giờ Việt Nam
            // trong API thanh toán đã làm.
            var orders = _context.Orders
                .AsNoTracking()
                .Where(o =>
                    o.OrderDate >= start &&
                    o.OrderDate < end);

            var summary = await orders
                .GroupBy(o => 1)
                .Select(group => new
                {
                    TotalOrders = group.Count(),
                    TotalRevenue = group.Sum(o => o.TotalAmount)
                })
                .FirstOrDefaultAsync();

            // Cộng số lượng theo ProductId để không bị tách
            // khi tên sản phẩm thay đổi.
            var bestSeller = await _context.OrderDetails
                .AsNoTracking()
                .Where(detail =>
                    detail.Order!.OrderDate >= start &&
                    detail.Order.OrderDate < end)
                .GroupBy(detail => detail.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(
                        detail => (long)detail.Quantity)
                })
                .OrderByDescending(item => item.Quantity)
                .ThenBy(item => item.ProductId)
                .FirstOrDefaultAsync();

            string bestSellerName = "Chưa có sản phẩm bán ra";
            long bestSellerQuantity = 0;

            if (bestSeller != null)
            {
                bestSellerQuantity = bestSeller.Quantity;

                // Lấy tên đã lưu trên chi tiết hóa đơn.
                bestSellerName = await _context.OrderDetails
                    .AsNoTracking()
                    .Where(detail =>
                        detail.ProductId == bestSeller.ProductId &&
                        detail.Order!.OrderDate >= start &&
                        detail.Order.OrderDate < end)
                    .OrderByDescending(detail => detail.Id)
                    .Select(detail => detail.ProductName)
                    .FirstAsync();
            }

            return Ok(new
            {
                ReportDate = start,
                TotalOrders = summary?.TotalOrders ?? 0,
                TotalRevenue = summary?.TotalRevenue ?? 0,
                BestSeller = bestSellerName,
                BestSellerQuantity = bestSellerQuantity
            });
        }

        // Tiết 3: gọi Stored Procedure bằng ADO.NET.
        [HttpGet("revenue-by-cashier")]
        public IActionResult RevenueByCashier() => ReadRevenue("dbo.sp_GetRevenueByCashier");

        [HttpGet("revenue-by-date-range")]
        public IActionResult RevenueByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate == default || toDate == default || fromDate.Date > toDate.Date
                || toDate.Date == DateTime.MaxValue.Date)
                return BadRequest(new { message = "Khoảng ngày không hợp lệ." });
            return ReadRevenue("dbo.sp_GetRevenueByDateRange", fromDate.Date, toDate.Date.AddDays(1));
        }

        private IActionResult ReadRevenue(string procedure, DateTime? start = null, DateTime? end = null)
        {
            var data = new List<CashierRevenueDto>();
            using var cn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand(procedure, cn) { CommandType = CommandType.StoredProcedure };
            if (start.HasValue) cmd.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = start.Value;
            if (end.HasValue) cmd.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = end.Value;
            cn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) data.Add(new CashierRevenueDto
            {
                CashierUsername = Convert.ToString(reader["CashierUsername"]) ?? "",
                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                TotalRevenue = reader["TotalRevenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalRevenue"]),
                TotalPointsAwarded = reader["TotalPointsAwarded"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalPointsAwarded"])
            });
            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now,
                totalCashiers = data.Count,
                grandTotalRevenue = data.Sum(x => x.TotalRevenue),
                data
            });
        }

        [HttpGet("top5-selling-products")]
        public IActionResult Top5SellingProducts()
        {
            var data = new List<object>();
            using var cn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("dbo.sp_GetTop5SellingProducts", cn)
            { CommandType = CommandType.StoredProcedure };
            cn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) data.Add(new
            {
                productId = Convert.ToInt32(reader["ProductId"]),
                productName = Convert.ToString(reader["ProductName"]) ?? "",
                totalQuantity = Convert.ToInt64(reader["TotalQuantity"]),
                totalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
            });
            return Ok(data);
        }
    }
}