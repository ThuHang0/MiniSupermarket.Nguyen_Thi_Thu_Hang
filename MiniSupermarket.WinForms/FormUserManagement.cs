using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private UserDto? _selectedUser;
        private bool _isBusy;

        public FormUserManagement()
        {
            InitializeComponent();
        }

        private async void FormUserManagement_Load(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission())
            {
                Close();
                return;
            }

            await RunAsync(LoadUsersAsync);
        }

        private bool HasPermission()
        {
            if (SessionManager.CurrentRole == "Admin")
            {
                return true;
            }

            MessageBox.Show(
                "Chỉ Admin được quản trị tài khoản.",
                "Từ chối truy cập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private async Task LoadUsersAsync()
        {
            using var response =
                await ApiClientService.Client.GetAsync("users");

            await EnsureSuccessAsync(response);

            var users = await response.Content
                .ReadFromJsonAsync<List<UserDto>>();

            if (IsDisposed)
            {
                return;
            }

            dgvUsers.DataSource = users ?? new List<UserDto>();
            dgvUsers.ClearSelection();

            ClearInputs();
        }

        private void dgvUsers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (_isBusy || e.RowIndex < 0)
            {
                return;
            }

            if (dgvUsers.Rows[e.RowIndex].DataBoundItem
                is not UserDto user)
            {
                return;
            }

            _selectedUser = user;

            txtUsername.Text = user.Username;
            txtFullName.Text = user.FullName;
            txtEmail.Text = user.Email ?? string.Empty;
            txtPassword.Clear();

            cboRole.SelectedItem = user.Role;

            // Các trường hồ sơ chỉ để xem khi chọn tài khoản.
            txtUsername.ReadOnly = true;
            txtFullName.ReadOnly = true;
            txtEmail.ReadOnly = true;
            cboRole.Enabled = false;

            btnAddUser.Enabled = false;
            btnResetPassword.Enabled = true;
            btnToggleLock.Enabled = true;

            btnToggleLock.Text = user.IsActive
                ? "Khóa tài khoản"
                : "Mở khóa tài khoản";

            lblSelectedUser.Text =
                $"Đã chọn: {user.Username}\n" +
                (user.IsActive ? "Đang hoạt động" : "Đang bị khóa");
        }

        private async void btnAddUser_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy || !HasPermission())
            {
                return;
            }

            if (_selectedUser != null)
            {
                MessageBox.Show(
                    "Bấm Nhập tài khoản mới trước khi thêm.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập và họ tên.");
                return;
            }

            if (!ValidatePassword())
            {
                return;
            }

            var request = new
            {
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),

                Email = string.IsNullOrWhiteSpace(txtEmail.Text)
                    ? null
                    : txtEmail.Text.Trim(),

                Password = txtPassword.Text,

                Role = cboRole.SelectedItem?.ToString()
                    ?? "Cashier"
            };

            await RunAsync(async () =>
            {
                using var response =
                    await ApiClientService.Client.PostAsJsonAsync(
                        "users",
                        request);

                await EnsureSuccessAsync(response);

                await LoadUsersAsync();

                if (!IsDisposed)
                {
                    MessageBox.Show("Tạo tài khoản thành công.");
                }
            });
        }

        private async void btnResetPassword_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy || !HasPermission())
            {
                return;
            }

            var user = _selectedUser;

            if (user == null)
            {
                MessageBox.Show("Hãy chọn tài khoản trong danh sách.");
                return;
            }

            if (!ValidatePassword())
            {
                return;
            }

            var confirm = MessageBox.Show(
                $"Đặt lại mật khẩu cho tài khoản {user.Username}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            string newPassword = txtPassword.Text;

            await RunAsync(async () =>
            {
                using var response =
                    await ApiClientService.Client.PutAsJsonAsync(
                        $"users/{user.Id}/reset-password",
                        new { Password = newPassword });

                await EnsureSuccessAsync(response);

                if (!IsDisposed)
                {
                    txtPassword.Clear();
                    MessageBox.Show("Đã đặt lại mật khẩu.");
                }
            });
        }

        private async void btnToggleLock_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy || !HasPermission())
            {
                return;
            }

            var user = _selectedUser;

            if (user == null)
            {
                MessageBox.Show("Hãy chọn tài khoản trong danh sách.");
                return;
            }

            string action = user.IsActive ? "khóa" : "mở khóa";

            var confirm = MessageBox.Show(
                $"Bạn muốn {action} tài khoản {user.Username}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            await RunAsync(async () =>
            {
                using var response =
                    await ApiClientService.Client.PutAsJsonAsync(
                        $"users/{user.Id}/toggle-lock",
                        new { });

                await EnsureSuccessAsync(response);

                await LoadUsersAsync();

                if (!IsDisposed)
                {
                    MessageBox.Show("Đã cập nhật trạng thái tài khoản.");
                }
            });
        }

        private void btnNew_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }

            ClearInputs();
            dgvUsers.ClearSelection();
            txtUsername.Focus();
        }

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission())
            {
                return;
            }

            await RunAsync(LoadUsersAsync);
        }

        private bool ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) ||
                txtPassword.Text.Length < 6)
            {
                MessageBox.Show(
                    "Mật khẩu phải có ít nhất 6 ký tự.");

                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            _selectedUser = null;

            txtUsername.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();

            txtUsername.ReadOnly = false;
            txtFullName.ReadOnly = false;
            txtEmail.ReadOnly = false;

            cboRole.Enabled = true;
            cboRole.SelectedItem = "Cashier";

            btnAddUser.Enabled = true;
            btnResetPassword.Enabled = false;
            btnToggleLock.Enabled = false;
            btnToggleLock.Text = "Khóa / Mở khóa";

            lblSelectedUser.Text = "Đang nhập tài khoản mới";
        }

        private async Task RunAsync(Func<Task> action)
        {
            if (_isBusy)
            {
                return;
            }

            _isBusy = true;
            Enabled = false;
            UseWaitCursor = true;

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Không thực hiện được",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isBusy = false;

                if (!IsDisposed)
                {
                    Enabled = true;
                    UseWaitCursor = false;
                }
            }
        }

        private static async Task EnsureSuccessAsync(
            HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                throw new InvalidOperationException(
                    "Phiên đăng nhập hết hạn. Hãy đăng nhập lại.");
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Forbidden)
            {
                throw new InvalidOperationException(
                    "Tài khoản không có quyền quản trị.");
            }

            string body =
                await response.Content.ReadAsStringAsync();

            string message =
                $"API trả lỗi HTTP {(int)response.StatusCode}.";

            try
            {
                using var document = JsonDocument.Parse(body);

                var root = document.RootElement;

                if (root.TryGetProperty("message", out var value))
                {
                    message = value.GetString() ?? message;
                }
                else if (root.TryGetProperty(
                    "errors", out var errors))
                {
                    message = string.Join(
                        Environment.NewLine,
                        errors.EnumerateObject()
                            .SelectMany(item =>
                                item.Value.EnumerateArray())
                            .Select(item =>
                                item.GetString() ?? string.Empty));
                }
            }
            catch (JsonException)
            {
            }

            throw new InvalidOperationException(message);
        }
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}