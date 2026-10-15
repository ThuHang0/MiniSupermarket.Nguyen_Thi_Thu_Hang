using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private bool _busy;

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(
            object? sender,
            EventArgs e)
        {
            if (SessionManager.CurrentRole != "Admin" &&
                SessionManager.CurrentRole != "Warehouse")
            {
                MessageBox.Show("Bạn không có quyền quản lý sản phẩm.");
                Close();
                return;
            }

            await RunAsync(async () =>
            {
                await LoadCategoriesAsync();
                await LoadProductsAsync();
            });
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await ApiClientService.Client
                .GetFromJsonAsync<List<CategoryDto>>("categories")
                ?? new List<CategoryDto>();

            // Hai danh sách riêng để lựa chọn ở hai ComboBox
            // không tác động lẫn nhau.
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";
            cboCategory.DataSource = categories;

            var filterCategories = new List<CategoryDto>
            {
                new CategoryDto
                {
                    CategoryId = 0,
                    CategoryName = "Tất cả danh mục"
                }
            };

            filterCategories.AddRange(categories);

            cboFilterCategory.DisplayMember = "CategoryName";
            cboFilterCategory.ValueMember = "CategoryId";
            cboFilterCategory.DataSource = filterCategories;
        }

        private async Task LoadProductsAsync()
        {
            string keyword = Uri.EscapeDataString(
                txtSearchBarcode.Text.Trim());

            int categoryId =
                cboFilterCategory.SelectedItem is CategoryDto category
                    ? category.CategoryId
                    : 0;

            string url =
                $"products?keyword={keyword}&categoryId={categoryId}";

            var products = await ApiClientService.Client
                .GetFromJsonAsync<List<ProductDto>>(url)
                ?? new List<ProductDto>();

            dgvProducts.DataSource = products;
            dgvProducts.ClearSelection();

            ClearInputs();
        }

        private void dgvProducts_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem
                is not ProductDto product)
            {
                return;
            }

            txtId.Text = product.ProductId.ToString();
            txtBarcode.Text = product.Barcode;
            txtProductName.Text = product.ProductName;
            nudPrice.Value = product.Price;
            nudStock.Value = product.StockQuantity;
            cboCategory.SelectedValue = product.CategoryId;
        }

        private ProductDto? ReadInputs()
        {
            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                MessageBox.Show("Vui lòng nhập mã vạch.");
                txtBarcode.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.");
                txtProductName.Focus();
                return null;
            }

            if (cboCategory.SelectedItem is not CategoryDto category)
            {
                MessageBox.Show("Vui lòng chọn danh mục.");
                return null;
            }

            return new ProductDto
            {
                Barcode = barcode,
                ProductName = name,
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = category.CategoryId
            };
        }

        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            var product = ReadInputs();

            if (product == null)
                return;

            await RunAsync(async () =>
            {
                using var response = await ApiClientService.Client
                    .PostAsJsonAsync("products", product);

                await EnsureSuccessAsync(response);

                await LoadProductsAsync();

                MessageBox.Show(
                    "Thêm sản phẩm thành công. " +
                    "Nếu đang lọc, sản phẩm có thể không nằm trong kết quả.");
            });
        }

        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn sản phẩm cần sửa.");
                return;
            }

            var product = ReadInputs();

            if (product == null)
                return;

            await RunAsync(async () =>
            {
                using var response = await ApiClientService.Client
                    .PutAsJsonAsync($"products/{id}", product);

                await EnsureSuccessAsync(response);

                await LoadProductsAsync();

                MessageBox.Show("Cập nhật sản phẩm thành công.");
            });
        }

        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn sản phẩm cần xóa.");
                return;
            }

            if (MessageBox.Show(
                $"Bạn có chắc muốn xóa \"{txtProductName.Text}\"?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            await RunAsync(async () =>
            {
                using var response = await ApiClientService.Client
                    .DeleteAsync($"products/{id}");

                await EnsureSuccessAsync(response);

                await LoadProductsAsync();

                MessageBox.Show("Đã xóa sản phẩm.");
            });
        }

        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            await RunAsync(LoadProductsAsync);
        }

        private async void txtSearchBarcode_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            await RunAsync(LoadProductsAsync);
        }

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            await RunAsync(async () =>
            {
                txtSearchBarcode.Clear();

                await LoadCategoriesAsync();

                cboFilterCategory.SelectedValue = 0;

                await LoadProductsAsync();
            });
        }

        private void btnClear_Click(
            object? sender,
            EventArgs e)
        {
            ClearInputs();
            dgvProducts.ClearSelection();
            txtBarcode.Focus();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
        }

        private async Task RunAsync(Func<Task> action)
        {
            if (_busy)
                return;

            _busy = true;
            SetButtonsEnabled(false);

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
                _busy = false;

                if (!IsDisposed)
                    SetButtonsEnabled(true);
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnAdd.Enabled = enabled;
            btnUpdate.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnSearch.Enabled = enabled;
            btnLoad.Enabled = enabled;
            btnClear.Enabled = enabled;
        }

        private static async Task EnsureSuccessAsync(
            HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string body = await response.Content.ReadAsStringAsync();

            string message = $"API trả lỗi {(int)response.StatusCode}.";

            try
            {
                using var document = JsonDocument.Parse(body);
                var root = document.RootElement;

                if (root.TryGetProperty("message", out var value))
                {
                    message = value.GetString() ?? message;
                }
                else if (root.TryGetProperty("errors", out var errors))
                {
                    message = string.Join(
                        Environment.NewLine,
                        errors.EnumerateObject()
                            .SelectMany(p => p.Value.EnumerateArray())
                            .Select(v => v.GetString() ?? ""));
                }
            }
            catch (JsonException)
            {
                // Giữ thông báo mặc định.
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                message = "Phiên đăng nhập hết hạn. Hãy đăng nhập lại.";
            }
            else if (response.StatusCode ==
                     System.Net.HttpStatusCode.Forbidden)
            {
                message = "Tài khoản không có quyền thực hiện.";
            }

            throw new InvalidOperationException(message);
        }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}