using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. GET /api/customers: Lấy toàn bộ danh sách khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.AsNoTracking().ToListAsync();
            return Ok(customers);
        }

        // 2. GET /api/customers/{id}: Lấy chi tiết thông tin khách hàng theo mã định danh
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng trong CSDL!" });
            }
            return Ok(customer);
        }

        // 3. GET /api/customers/search?keyword=...: Tìm kiếm khách hàng theo tên hoặc số điện thoại
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm!" });
            }

            // Tìm kiếm tương đối (LIKE) trên cả Tên và Số điện thoại
            var result = await _context.Customers
                .Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword))
                .ToListAsync();

            return Ok(result);
        }

        // 4. POST /api/customers: Thêm mới khách hàng thành viên
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào SQL Server

            return CreatedAtAction(nameof(GetById), new { id = newCustomer.CustomerId }, newCustomer);
        }

        // 5. PUT /api/customers/{id}: Cập nhật thông tin và hạng thẻ của khách hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng cần sửa!" });
            }

            // Cập nhật các trường
            customer.CustomerName = updatedCustomer.CustomerName;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;
            customer.Address = updatedCustomer.Address;
            customer.RewardPoints = updatedCustomer.RewardPoints;
            customer.MembershipRank = updatedCustomer.MembershipRank;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE /api/customers/{id}: Xóa tài khoản khách hàng khỏi hệ thống
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { message = "Không tìm thấy khách hàng cần xóa!" });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}