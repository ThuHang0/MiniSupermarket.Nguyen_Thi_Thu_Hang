using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        // Bổ sung phương thức cấu hình HttpClient có gắn kèm Token bảo mật


        public FormCategoryManagement()
        {
            InitializeComponent();
        }

        // Sự kiện Form vừa bật lên: Tự động tải dữ liệu từ API lên bảng
        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Hàm dùng chung: Gọi API GET lấy danh sách và đổ lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                var categories = await ApiClientService.Client
                    .GetFromJsonAsync<List<CategoryDto>>("categories");
                dgvCategories.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quyền truy cập hoặc mất kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task SaveCategoryAsync(
    Func<Task<HttpResponseMessage>> sendRequest,
    string successMessage)
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;

            try
            {
                using var response = await sendRequest();

                if (!response.IsSuccessStatusCode)
                {
                    string message =
                        await ApiClientService.ReadErrorMessageAsync(response);

                    MessageBox.Show(
                        message,
                        "Không thể lưu danh mục",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    successMessage,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearInputs();
                await LoadDataAsync();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Không kết nối được API. Hãy kiểm tra kết nối " +
                    "và tải lại danh sách trước khi thử lại.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Yêu cầu quá thời gian chờ. Hãy tải lại danh sách " +
                    "để kiểm tra dữ liệu đã được lưu chưa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không hoàn tất được thao tác: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                {
                    btnAdd.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
        }

        // Nút Tải lại dữ liệu (Refresh)
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Sự kiện khi click vào một dòng trên DataGridView: Đưa dữ liệu lên các ô nhập (TextBox) để chuẩn bị Sửa/Xóa
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                !dgvCategories.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

                // ĐÃ FIX: Sử dụng đúng tên Name của các cột đã thiết lập trong Designer
                txtId.Text = row.Cells["colCategoryId"].Value?.ToString() ?? string.Empty;
                txtCategoryName.Text = row.Cells["colCategoryName"].Value?.ToString() ?? string.Empty;
                txtDescription.Text = row.Cells["colDescription"].Value?.ToString() ?? string.Empty;
            }
        }

        // Nút THÊM MỚI (CREATE): Gửi dữ liệu POST lên Web API
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                txtCategoryName.Focus();
                return;
            }

            if (name.Length > 100)
            {
                MessageBox.Show("Tên danh mục không được vượt quá 100 ký tự.");
                txtCategoryName.Focus();
                return;
            }

            if (description.Length > 255)
            {
                MessageBox.Show("Mô tả không được vượt quá 255 ký tự.");
                txtDescription.Focus();
                return;
            }

            var category = new
            {
                CategoryName = name,
                Description = description
            };

            await SaveCategoryAsync(
                () => ApiClientService.Client.PostAsJsonAsync(
                    "categories",
                    category),
                "Thêm danh mục thành công!");
        }

        // Nút CẬP NHẬT (UPDATE): Gửi dữ liệu PUT lên Web API theo ID
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.");
                return;
            }

            string name = txtCategoryName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                txtCategoryName.Focus();
                return;
            }

            if (name.Length > 100)
            {
                MessageBox.Show("Tên danh mục không được vượt quá 100 ký tự.");
                txtCategoryName.Focus();
                return;
            }

            if (description.Length > 255)
            {
                MessageBox.Show("Mô tả không được vượt quá 255 ký tự.");
                txtDescription.Focus();
                return;
            }

            var category = new
            {
                CategoryId = id,
                CategoryName = name,
                Description = description
            };

            await SaveCategoryAsync(
                () => ApiClientService.Client.PutAsJsonAsync(
                    $"categories/{id}",
                    category),
                "Cập nhật danh mục thành công!");
        }
        // Nút XÓA (DELETE): Gửi request DELETE lên Web API theo ID
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa danh mục ID = {id}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            try
            {
                using var response = await ApiClientService.Client
                    .DeleteAsync($"categories/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    string message =
                        await ApiClientService.ReadErrorMessageAsync(response);

                    MessageBox.Show(
                        message,
                        "Không thể xóa danh mục",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                ClearInputs();

                MessageBox.Show(
                    "Xóa danh mục thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadDataAsync();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Không kết nối được máy chủ. " +
                    "Hãy kiểm tra API và tải lại danh sách để xác nhận kết quả.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Yêu cầu quá thời gian chờ. " +
                    "Hãy tải lại danh sách để kiểm tra kết quả xóa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không hoàn tất được thao tác: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    btnDelete.Enabled = true;
            }
        }

        // Nút TÌM KIẾM (SEARCH): Gọi API lọc danh mục theo từ khóa Query String
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadDataAsync();
                return;
            }

            btnSearch.Enabled = false;

            try
            {
                using var response = await ApiClientService.Client.GetAsync(
                    $"categories/search?keyword={Uri.EscapeDataString(keyword)}");

                if (!response.IsSuccessStatusCode)
                {
                    string message =
                        await ApiClientService.ReadErrorMessageAsync(response);

                    MessageBox.Show(
                        message,
                        "Không thể tìm kiếm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var result = await response.Content
                    .ReadFromJsonAsync<List<CategoryDto>>();

                dgvCategories.DataSource = result ?? new List<CategoryDto>();

                if (result == null || result.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy danh mục phù hợp.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Không kết nối được API. Hãy kiểm tra máy chủ.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Tìm kiếm quá thời gian chờ. Vui lòng thử lại.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thực hiện được tìm kiếm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    btnSearch.Enabled = true;
            }
        }

        // Hàm phụ trợ: Xóa trắng các ô nhập liệu sau khi thao tác xong
        private void ClearInputs()
        {
            txtId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";
        }

        private void gbSearch_Enter(object sender, EventArgs e)
        {

        }
    }

    // Lớp DTO trung gian tại Client hứng dữ liệu JSON trả về từ Server
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}