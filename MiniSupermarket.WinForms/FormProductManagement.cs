
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private void panelRight_Paint(object sender, PaintEventArgs e)
        {
        }
        private List<ProductViewDto> _products = new();

        public FormProductManagement()
        {
            InitializeComponent();
            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new System.Drawing.Point(470, 48);
            btnSearch.Size = new System.Drawing.Size(110, 35);
            btnSearch.ForeColor = System.Drawing.Color.White;
            btnSearch.BringToFront();

            Load += FormProductManagement_Load;

            btnLoad.Click += btnLoad_Click;
            btnSearch.Click += btnSearch_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;

            dgvProducts.CellClick += dgvProducts_CellClick;
        }

        private async void FormProductManagement_Load(
            object? sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        // Tải danh mục từ API
        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories =
                    await ApiClientService.Client
                    .GetFromJsonAsync<List<CategoryViewDto>>(
                        "categories");

                categories ??= new();

                cboCategory.DataSource =
                    new List<CategoryViewDto>(categories);

                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                var filterItems =
                    new List<CategoryViewDto>
                    {
                        new CategoryViewDto
                        {
                            CategoryId = 0,
                            CategoryName = "Tất cả danh mục"
                        }
                    };

                filterItems.AddRange(categories);

                cboFilterCategory.DataSource = filterItems;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
                cboFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh mục: " + ex.Message);
            }
        }

        // Tải sản phẩm từ API
        private async Task LoadProductsAsync()
        {
            try
            {
                _products = await ApiClientService.Client
                    .GetFromJsonAsync<List<ProductViewDto>>(
                        "products") ?? new();

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được sản phẩm: " + ex.Message);
            }
        }

        // Chọn dòng sản phẩm
        private void dgvProducts_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem
                is not ProductViewDto product)
                return;

            txtId.Text = product.ProductId.ToString();
            txtBarcode.Text = product.Barcode;
            txtProductName.Text = product.ProductName;

            nudPrice.Value = Math.Clamp(
                product.Price,
                nudPrice.Minimum,
                nudPrice.Maximum);

            nudStock.Value = Math.Clamp(
                product.StockQuantity,
                0,
                (int)nudStock.Maximum);

            cboCategory.SelectedValue = product.CategoryId;
        }

        // Tạo dữ liệu gửi cho API
        private object GetProductInput(int? id = null)
        {
            return new
            {
                ProductId = id ?? 0,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = Convert.ToInt32(
                    cboCategory.SelectedValue)
            };
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã vạch và tên sản phẩm!");
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Giá phải lớn hơn 0!");
                return false;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục!");
                return false;
            }

            return true;
        }

        // Thêm sản phẩm
        private async void btnAdd_Click(
            object? sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var response = await ApiClientService.Client
                    .PostAsJsonAsync(
                        "products",
                        GetProductInput());

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Thêm thất bại: " +
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Cập nhật sản phẩm
        private async void btnUpdate_Click(
            object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn sản phẩm cần sửa!");
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                var response = await ApiClientService.Client
                    .PutAsJsonAsync(
                        $"products/{id}",
                        GetProductInput(id));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    await LoadProductsAsync();
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật thất bại: " +
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Xóa sản phẩm
        private async void btnDelete_Click(
            object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn sản phẩm cần xóa!");
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc chắn muốn xóa?",
                "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                var response =
                    await ApiClientService.Client
                    .DeleteAsync($"products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa thành công!");
                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Xóa thất bại: " +
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Tìm kiếm, lọc danh mục
        private void btnSearch_Click(
            object? sender, EventArgs e)
        {
            string keyword =
                txtSearchBarcode.Text.Trim();

            int categoryId = Convert.ToInt32(
                cboFilterCategory.SelectedValue ?? 0);

            var filtered = _products.Where(p =>
                (string.IsNullOrEmpty(keyword) ||
                 p.Barcode.Contains(
                     keyword,
                     StringComparison.OrdinalIgnoreCase) ||
                 p.ProductName.Contains(
                     keyword,
                     StringComparison.OrdinalIgnoreCase)) &&
                (categoryId == 0 ||
                 p.CategoryId == categoryId))
                .ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filtered;
        }

        private async void btnLoad_Click(
            object? sender, EventArgs e)
        {
            await LoadProductsAsync();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class ProductViewDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = "";
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }

    public class CategoryViewDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
    }
}
