using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint Đăng nhập: POST /api/auth/login

        [HttpPost("login")]
        public IActionResult Login(
            [FromBody] LoginRequestDto request)
        {
            var accounts = new[]
            {
        new { Username = "admin01", FullName = "Nguyễn Quản Trị", Role = "Admin" },
        new { Username = "admin02", FullName = "Trần Giám Đốc", Role = "Admin" },

        new { Username = "cashier01", FullName = "Lê Thu Ngân", Role = "Cashier" },
        new { Username = "cashier02", FullName = "Phạm Bán Hàng", Role = "Cashier" },
        new { Username = "cashier03", FullName = "Hoàng Thu Ngân", Role = "Cashier" },
        new { Username = "cashier04", FullName = "Vũ Thị Quầy", Role = "Cashier" },
        new { Username = "cashier05", FullName = "Đỗ Bán Lẻ", Role = "Cashier" },

        new { Username = "ware01", FullName = "Ngô Quản Kho", Role = "Warehouse" },
        new { Username = "ware02", FullName = "Bùi Kiểm Kê", Role = "Warehouse" },
        new { Username = "ware03", FullName = "Dương Thủ Kho", Role = "Warehouse" },
        new { Username = "ware04", FullName = "Lý Nhập Hàng", Role = "Warehouse" },

        new { Username = "admin_backup", FullName = "Đặng Hỗ Trợ", Role = "Admin" },
        new { Username = "cashier06", FullName = "Hồ Ca Chiều", Role = "Cashier" },
        new { Username = "ware05", FullName = "Trương Vận Chuyển", Role = "Warehouse" },
        new { Username = "supervisor", FullName = "Mai Giám Sát", Role = "Admin" }
    };

            var user = accounts.FirstOrDefault(x =>
                x.Username.Equals(
                    request.Username,
                    StringComparison.OrdinalIgnoreCase));

            if (user == null || request.Password != "123456")
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            var token = GenerateJwtToken(
                user.Username,
                user.Role);

            return Ok(new
            {
                success = true,
                token = token,
                username = user.Username,
                fullName = user.FullName,
                role = user.Role
            });
        }


        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Lấy khóa bí mật từ appsettings.json
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Thời hạn token là 2 tiếng
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
