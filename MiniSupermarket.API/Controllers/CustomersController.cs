using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
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
        public async Task<IActionResult> Create(
            [FromBody] Customer newCustomer)
        {
            string phone = newCustomer.PhoneNumber.Trim();

            if (await PhoneExistsAsync(phone))
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được đăng ký cho khách hàng khác."
                });
            }
            bool isAdmin = User.IsInRole("Admin");

            if (newCustomer.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm."
                });
            }

            if (!isAdmin &&
                (newCustomer.RewardPoints != 0 ||
                 newCustomer.MembershipRank != "Chuẩn"))
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = "Khách hàng do thu ngân tạo phải có " +
                                  "0 điểm và hạng Chuẩn."
                    });
            }

            // Tạo Entity mới để không nhận ID do client gửi lên.
            var customer = new Customer
            {
                CustomerName = newCustomer.CustomerName.Trim(),
                PhoneNumber = phone,
                Address = newCustomer.Address?.Trim(),
                RewardPoints = isAdmin ? newCustomer.RewardPoints : 0,
                MembershipRank = isAdmin
                    ? newCustomer.MembershipRank.Trim()
                    : "Chuẩn"
            };

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sql &&
                      (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new
                {
                    message = "Thông tin khách hàng bị trùng. " +
                              "Vui lòng kiểm tra lại số điện thoại."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer);
        }
        private async Task<bool> PhoneExistsAsync(
        string phone,
        int? excludedCustomerId = null)
        {
            return await _context.Customers.AnyAsync(c =>
                c.PhoneNumber == phone &&
                (!excludedCustomerId.HasValue ||
                 c.CustomerId != excludedCustomerId.Value));
        }
        // 5. PUT /api/customers/{id}: Cập nhật thông tin và hạng thẻ của khách hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần sửa."
                });
            }

            bool isAdmin = User.IsInRole("Admin");


            // Cashier không được thay đổi điểm và hạng thẻ.
            if (!isAdmin &&
                (updatedCustomer.RewardPoints != customer.RewardPoints ||
                 updatedCustomer.MembershipRank != customer.MembershipRank))
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new
                    {
                        message = "Chỉ Admin được điều chỉnh điểm thưởng " +
                                  "và hạng khách hàng."
                    });
            }
            string phone = updatedCustomer.PhoneNumber.Trim();

            if (await PhoneExistsAsync(phone, id))
            {
                return Conflict(new
                {
                    message = "Số điện thoại đã được đăng ký cho khách hàng khác."
                });
            }

            if (isAdmin && updatedCustomer.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm."
                });
            }

            customer.CustomerName = updatedCustomer.CustomerName.Trim();
            customer.PhoneNumber = phone;
            customer.Address = updatedCustomer.Address?.Trim();

            if (isAdmin)
            {
                customer.RewardPoints = updatedCustomer.RewardPoints;
                customer.MembershipRank =
                    updatedCustomer.MembershipRank.Trim();
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sql &&
                      (sql.Number == 2601 || sql.Number == 2627))
            {
                return Conflict(new
                {
                    message = "Thông tin khách hàng bị trùng. " +
                              "Vui lòng kiểm tra lại số điện thoại."
                });
            }
        }

        // 6. DELETE /api/customers/{id}: Xóa tài khoản khách hàng khỏi hệ thống
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa."
                });
            }

            bool hasOrders = await _context.Orders
                .AnyAsync(o => o.CustomerId == id);

            if (hasOrders)
            {
                return Conflict(new
                {
                    message = "Khách hàng đã có hóa đơn. " +
                              "Không thể xóa để bảo toàn lịch sử mua hàng."
                });
            }

            _context.Customers.Remove(customer);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sql && sql.Number == 547)
            {
                return Conflict(new
                {
                    message = "Khách hàng đang được sử dụng, không thể xóa."
                });
            }
        }
    }
}