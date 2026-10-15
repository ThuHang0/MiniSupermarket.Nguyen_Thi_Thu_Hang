using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;

        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST /api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            string username = request.Username.Trim();

            // 1. Tìm tài khoản trong SQL Server.
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Username == username);

            // 2. Kiểm tra tài khoản và mật khẩu.
            if (user == null ||
                !VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // 3. Kiểm tra trạng thái tài khoản.
            if (!user.IsActive)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    success = false,
                    message = "Tài khoản đã bị khóa!"
                });
            }

            // 4. Chỉ chấp nhận ba vai trò của hệ thống.
            if (user.Role != "Admin" &&
                user.Role != "Cashier" &&
                user.Role != "Warehouse")
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    success = false,
                    message = "Tài khoản chưa được cấp vai trò hợp lệ!"
                });
            }

            // 5. Tạo JWT từ thông tin tài khoản trong database.
            DateTime expiresAt = DateTime.UtcNow.AddHours(2);

            string token = GenerateJwtToken(
                user.Id,
                user.Username,
                user.Role,
                expiresAt
            );

            // 6. Trả kết quả cho WinForms.
            return Ok(new
            {
                success = true,
                token,
                username = user.Username,
                fullName = user.FullName,
                role = user.Role,
                expiresAt
            });
        }

        private static bool VerifyPassword(
            string password,
            string storedPasswordHash)
        {
            // Định dạng đã lưu ở Phần 1:
            // iterations.salt.hash
            string[] parts = storedPasswordHash.Split('.');

            if (parts.Length != 3)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out int iterations) ||
                iterations < 10000 ||
                iterations > 1000000)
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] expectedHash = Convert.FromBase64String(parts[2]);

                if (salt.Length < 16 || expectedHash.Length != 32)
                {
                    return false;
                }

                byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    expectedHash.Length
                );

                return CryptographicOperations.FixedTimeEquals(
                    actualHash,
                    expectedHash
                );
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private string GenerateJwtToken(
            int userId,
            string username,
            string role,
            DateTime expiresAt)
        {
            string secret =
                _configuration["JwtSettings:Secret"]
                ?? throw new InvalidOperationException(
                    "Chưa cấu hình JwtSettings:Secret."
                );

            byte[] key = Encoding.UTF8.GetBytes(secret);

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userId.ToString()
                ),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                )
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; } = string.Empty;
    }
}





//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace MiniSupermarket.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;

//        public AuthController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        // Endpoint Đăng nhập: POST /api/auth/login

//        [HttpPost("login")]
//        public IActionResult Login(
//            [FromBody] LoginRequestDto request)
//        {
//            var accounts = new[]
//            {
//        new { Username = "admin01", FullName = "Nguyễn Quản Trị", Role = "Admin" },
//        new { Username = "admin02", FullName = "Trần Giám Đốc", Role = "Admin" },

//        new { Username = "cashier01", FullName = "Lê Thu Ngân", Role = "Cashier" },
//        new { Username = "cashier02", FullName = "Phạm Bán Hàng", Role = "Cashier" },
//        new { Username = "cashier03", FullName = "Hoàng Thu Ngân", Role = "Cashier" },
//        new { Username = "cashier04", FullName = "Vũ Thị Quầy", Role = "Cashier" },
//        new { Username = "cashier05", FullName = "Đỗ Bán Lẻ", Role = "Cashier" },

//        new { Username = "ware01", FullName = "Ngô Quản Kho", Role = "Warehouse" },
//        new { Username = "ware02", FullName = "Bùi Kiểm Kê", Role = "Warehouse" },
//        new { Username = "ware03", FullName = "Dương Thủ Kho", Role = "Warehouse" },
//        new { Username = "ware04", FullName = "Lý Nhập Hàng", Role = "Warehouse" },

//        new { Username = "admin_backup", FullName = "Đặng Hỗ Trợ", Role = "Admin" },
//        new { Username = "cashier06", FullName = "Hồ Ca Chiều", Role = "Cashier" },
//        new { Username = "ware05", FullName = "Trương Vận Chuyển", Role = "Warehouse" },
//        new { Username = "supervisor", FullName = "Mai Giám Sát", Role = "Admin" }
//    };

//            var user = accounts.FirstOrDefault(x =>
//                x.Username.Equals(
//                    request.Username,
//                    StringComparison.OrdinalIgnoreCase));

//            if (user == null || request.Password != "123456")
//            {
//                return Unauthorized(new
//                {
//                    success = false,
//                    message = "Sai tài khoản hoặc mật khẩu!"
//                });
//            }

//            var token = GenerateJwtToken(
//                user.Username,
//                user.Role);

//            return Ok(new
//            {
//                success = true,
//                token = token,
//                username = user.Username,
//                fullName = user.FullName,
//                role = user.Role
//            });
//        }


//        private string GenerateJwtToken(string username, string role)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            // Lấy khóa bí mật từ appsettings.json
//            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new[] {
//                    new Claim(ClaimTypes.Name, username),
//                    new Claim(ClaimTypes.Role, role)
//                }),
//                Expires = DateTime.UtcNow.AddHours(2), // Thời hạn token là 2 tiếng
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }

//    public class LoginRequestDto
//    {
//        public string Username { get; set; } = string.Empty;
//        public string Password { get; set; } = string.Empty;
//    }
//}
