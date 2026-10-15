using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
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
    }
}