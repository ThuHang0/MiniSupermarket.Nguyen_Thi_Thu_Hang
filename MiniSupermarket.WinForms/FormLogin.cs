using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        private bool _isLoggingIn;

        public FormLogin()
        {
            InitializeComponent();

            txtPass.UseSystemPasswordChar = true;
            AcceptButton = btnLogin;
        }

        private async void btnLogin_Click(
            object sender,
            EventArgs e)
        {
            if (_isLoggingIn)
                return;

            string username = txtUser.Text.Trim();

            // Không Trim mật khẩu để giữ đúng ký tự người dùng nhập.
            string password = txtPass.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tài khoản và mật khẩu.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            _isLoggingIn = true;
            btnLogin.Enabled = false;

            try
            {
                ApiClientService.Logout();

                using var response =
                    await ApiClientService.Client.PostAsJsonAsync(
                        "auth/login",
                        new
                        {
                            Username = username,
                            Password = password
                        });

                if (!response.IsSuccessStatusCode)
                {
                    string message = await ReadErrorMessageAsync(
                        response);

                    MessageBox.Show(
                        message,
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var result =
                    await response.Content
                        .ReadFromJsonAsync<LoginResponseDto>();

                if (result == null ||
                    !result.Success ||
                    string.IsNullOrWhiteSpace(result.Token))
                {
                    MessageBox.Show(
                        "Dữ liệu đăng nhập trả về không hợp lệ.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                SessionManager.JwtToken = result.Token;
                SessionManager.CurrentUsername = result.Username;
                SessionManager.CurrentFullName = result.FullName;
                SessionManager.CurrentRole = result.Role;

                ApiClientService.SetToken(result.Token);

                Hide();

                try
                {
                    using var shell = new FormMainShell();
                    shell.ShowDialog();
                }
                finally
                {
                    ApiClientService.Logout();

                    txtPass.Clear();

                    Show();
                    Activate();
                    txtPass.Focus();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không kết nối được API. " +
                    "Hãy kiểm tra API đang chạy tại cổng 7063." +
                    Environment.NewLine +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Yêu cầu quá thời gian chờ. Hãy thử lại.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isLoggingIn = false;
                btnLogin.Enabled = true;
            }
        }

        private static async Task<string> ReadErrorMessageAsync(
            HttpResponseMessage response)
        {
            string body =
                await response.Content.ReadAsStringAsync();

            try
            {
                using var document = JsonDocument.Parse(body);

                if (document.RootElement.TryGetProperty(
                    "message", out var message))
                {
                    return message.GetString()
                        ?? "Đăng nhập thất bại.";
                }
            }
            catch (JsonException)
            {
                // Dùng thông báo mặc định nếu phản hồi không phải JSON.
            }

            return $"Đăng nhập thất bại. Mã lỗi: " +
                $"{(int)response.StatusCode}.";
        }

        // Giữ các sự kiện mà Designer hiện tại đang tham chiếu.
        private void pnlMain_Paint(
            object sender,
            PaintEventArgs e)
        {
        }

        private void pnlLogin_Paint(
            object sender,
            PaintEventArgs e)
        {
        }
    }
}