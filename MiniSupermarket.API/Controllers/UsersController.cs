using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public UsersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Kiểm tra Admin hiện tại vẫn đang hoạt động.
        private async Task<bool> IsActiveAdminAsync()
        {
            string? idText =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(idText, out int id))
            {
                return false;
            }

            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u =>
                    u.Id == id &&
                    u.IsActive &&
                    u.Role == "Admin");
        }

        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!await IsActiveAdminAsync())
            {
                return Forbid();
            }

            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Email,
                    u.Role,
                    u.IsActive
                })
                .ToListAsync();

            // Không trả PasswordHash về client.
            return Ok(users);
        }

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserRequest request)
        {
            if (!await IsActiveAdminAsync())
            {
                return Forbid();
            }

            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message =
                        "Tên đăng nhập, họ tên và mật khẩu không được trống."
                });
            }

            if (request.Role != "Admin" &&
                request.Role != "Cashier" &&
                request.Role != "Warehouse")
            {
                return BadRequest(new
                {
                    message = "Vai trò không hợp lệ."
                });
            }

            string username = request.Username.Trim();

            bool exists = await _context.Users
                .AnyAsync(u => u.Username == username);

            if (exists)
            {
                return Conflict(new
                {
                    message = "Tên đăng nhập đã tồn tại."
                });
            }

            var user = new User
            {
                Username = username,
                FullName = request.FullName.Trim(),

                Email = string.IsNullOrWhiteSpace(request.Email)
                    ? null
                    : request.Email.Trim(),

                Role = request.Role,
                IsActive = true,
                PasswordHash = HashPassword(request.Password)
            };

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không lưu được tài khoản. " +
                        "Hãy tải lại và kiểm tra tên đăng nhập."
                });
            }

            return Ok(new
            {
                user.Id,
                message = "Tạo tài khoản thành công."
            });
        }

        // PUT /api/users/1/reset-password
        [HttpPut("{id:int}/reset-password")]
        public async Task<IActionResult> ResetPassword(
            int id,
            [FromBody] ResetPasswordRequest request)
        {
            if (!await IsActiveAdminAsync())
            {
                return Forbid();
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Mật khẩu không được để trống."
                });
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản."
                });
            }

            user.PasswordHash = HashPassword(request.Password);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT /api/users/1/toggle-lock
        [HttpPut("{id:int}/toggle-lock")]
        public async Task<IActionResult> ToggleLock(int id)
        {
            if (!await IsActiveAdminAsync())
            {
                return Forbid();
            }

            int currentId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (id == currentId)
            {
                return BadRequest(new
                {
                    message =
                        "Không thể tự khóa tài khoản đang đăng nhập."
                });
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản."
                });
            }

            if (user.IsActive && user.Role == "Admin")
            {
                int activeAdmins = await _context.Users
                    .CountAsync(u =>
                        u.IsActive && u.Role == "Admin");

                if (activeAdmins <= 1)
                {
                    return BadRequest(new
                    {
                        message =
                            "Phải còn ít nhất một Admin hoạt động."
                    });
                }
            }

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                user.IsActive,
                message = user.IsActive
                    ? "Đã mở khóa tài khoản."
                    : "Đã khóa tài khoản."
            });
        }

        private static string HashPassword(string password)
        {
            // Cùng định dạng với VerifyPassword trong AuthController:
            // iterations.salt.hash
            byte[] salt = RandomNumberGenerator.GetBytes(24);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32);

            return "100000." +
                   Convert.ToBase64String(salt) + "." +
                   Convert.ToBase64String(hash);
        }
    }

    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }

    public class ResetPasswordRequest
    {
        [Required]
        [StringLength(128, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}