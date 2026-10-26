using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;

        public static string CurrentUsername { get; set; } = string.Empty;

        public static string CurrentFullName { get; set; } = string.Empty;

        public static string CurrentRole { get; set; } = string.Empty;

        public static void Clear()
        {
            JwtToken = string.Empty;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
        }
    }

    public static class ApiClientService
    {
        public static async Task<string> ReadErrorMessageAsync(
    HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return "Phiên đăng nhập không hợp lệ hoặc đã hết hạn. " +
                       "Vui lòng đăng nhập lại.";
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return "Bạn không có quyền thực hiện chức năng này.";
            }

            string body = await response.Content.ReadAsStringAsync();

            try
            {
                using var document = JsonDocument.Parse(body);
                var root = document.RootElement;

                if (root.ValueKind == JsonValueKind.Object &&
                    root.TryGetProperty("message", out var message) &&
                    message.ValueKind == JsonValueKind.String)
                {
                    string? text = message.GetString();

                    if (!string.IsNullOrWhiteSpace(text))
                        return text;
                }
            }
            catch (JsonException)
            {
                // Phản hồi không phải JSON thì dùng thông báo bên dưới.
            }

            return $"Không thực hiện được yêu cầu. " +
                   $"Mã lỗi HTTP: {(int)response.StatusCode}.";
        }

        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7063/api/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static void SetToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", token);
        }

        public static void Logout()
        {
            SessionManager.Clear();
            SetToken(string.Empty);
        }

        // Giữ phương thức này để tương thích nếu code cũ đang gọi.
        public static async Task<string> GetDataWithTokenAsync(
            string endpoint)
        {
            SetToken(SessionManager.JwtToken);

            using var response = await Client.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }

        public string Token { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}




//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text.Json;

//namespace MiniSupermarket.WinForms
//{
//    public static class SessionManager
//    {
//        public static string JwtToken { get; set; }
//            = string.Empty;

//        public static string CurrentRole { get; set; }
//            = string.Empty;

//        public static string CurrentUsername { get; set; }
//            = string.Empty;
//    }

//    public static class ApiClientService
//    {
//        private static readonly HttpClient _client = new HttpClient
//        {
//            /*BaseAddress = new Uri("https://localhost:7123/api/")*/  // nhớ chỉnh port phù hợp
//            BaseAddress = new Uri("https://localhost:7063/api/")
//        };
//        public static HttpClient Client
//        {
//            get
//            {
//                _client.DefaultRequestHeaders.Authorization =
//                    string.IsNullOrWhiteSpace(SessionManager.JwtToken)
//                        ? null
//                        : new AuthenticationHeaderValue(
//                            "Bearer",
//                            SessionManager.JwtToken);

//                return _client;
//            }
//        }
//        // Hàm gọi API đăng nhập lấy Token
//        public static async Task<bool> LoginAsync(string username, string password)
//        {
//            var loginObj = new { Username = username, Password = password };
//            var response = await _client.PostAsJsonAsync("auth/login", loginObj);

//            if (response.IsSuccessStatusCode)
//            {
//                var jsonString = await response.Content.ReadAsStringAsync();
//                using var doc = JsonDocument.Parse(jsonString);
//                SessionManager.JwtToken = doc.RootElement.GetProperty("token").GetString() ?? string.Empty;
//                SessionManager.CurrentRole = doc.RootElement.GetProperty("role").GetString() ?? string.Empty;
//                return true;
//            }
//            return false;
//        }

//        // Hàm gọi API lấy dữ liệu có gắn kèm Bearer Token bảo mật
//        public static async Task<string> GetDataWithTokenAsync(string endpoint)
//        {
//            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
//            var response = await _client.GetAsync(endpoint);

//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadAsStringAsync();
//            }
//            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
//            {
//                throw new Exception("Phiên làm việc hết hạn hoặc chưa đăng nhập!");
//            }
//            throw new Exception("Lỗi khi gọi dữ liệu từ Server.");
//        }
//    }
//}
