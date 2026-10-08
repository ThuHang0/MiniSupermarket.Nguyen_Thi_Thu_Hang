using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly BindingList<PosCartItem> _cart = new();
        private readonly BindingSource _cartSource = new();

        private bool _isBusy;

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();

            _cartSource.DataSource = _cart;
            dgvCart.DataSource = _cartSource;

            // Chỉ đăng ký sự kiện ở đây.
            Load += FormPOS_Load;
            KeyDown += FormPOS_KeyDown;

            txtBarcode.KeyDown += txtBarcode_KeyDown;
            btnAddBarcode.Click += btnAddBarcode_Click;

            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;
            txtCustomerPhone.TextChanged +=
                txtCustomerPhone_TextChanged;
            btnFindCustomer.Click += btnFindCustomer_Click;

            txtCashReceived.TextChanged +=
                txtCashReceived_TextChanged;

            btnRemoveItem.Click += btnRemoveItem_Click;
            btnClearCart.Click += btnClearCart_Click;
            btnCheckout.Click += btnCheckout_Click;

            UpdateCartDisplay();
        }

        private bool HasPosPermission()
        {
            bool allowed =
                string.Equals(
                    SessionManager.CurrentRole,
                    "Admin",
                    StringComparison.OrdinalIgnoreCase)
                ||
                string.Equals(
                    SessionManager.CurrentRole,
                    "Cashier",
                    StringComparison.OrdinalIgnoreCase);

            if (!allowed)
            {
                MessageBox.Show(
                    "Chỉ Admin và Cashier được sử dụng POS!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return allowed;
        }

        private void FormPOS_Load(object? sender, EventArgs e)
        {
            if (!HasPosPermission())
            {
                Close();
                return;
            }

            txtBarcode.Focus();
        }

        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductId",
                DataPropertyName = nameof(PosCartItem.ProductId),
                HeaderText = "Mã SP",
                Width = 65
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                DataPropertyName = nameof(PosCartItem.ProductName),
                HeaderText = "Tên sản phẩm",
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 120
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                DataPropertyName = nameof(PosCartItem.UnitPrice),
                HeaderText = "Đơn giá",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment =
                        DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity",
                DataPropertyName = nameof(PosCartItem.Quantity),
                HeaderText = "SL",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment =
                        DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalPrice",
                DataPropertyName = nameof(PosCartItem.TotalPrice),
                HeaderText = "Thành tiền",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment =
                        DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        // ================= THÊM SẢN PHẨM =================

        private async void txtBarcode_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            await AddBarcodeAsync();
        }

        private async void btnAddBarcode_Click(
            object? sender,
            EventArgs e)
        {
            await AddBarcodeAsync();
        }

        private async Task AddBarcodeAsync()
        {
            if (_isBusy || !HasPosPermission())
                return;

            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                txtBarcode.Focus();
                return;
            }

            SetBusy(true);

            try
            {
                string endpoint =
                    "products/barcode/"
                    + Uri.EscapeDataString(barcode);

                using var response =
                    await ApiClientService.Client.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync(
                        response,
                        "Không tìm thấy hoặc không tải được sản phẩm.");

                    return;
                }

                var product = await response.Content
                    .ReadFromJsonAsync<PosProductDto>();

                if (product == null)
                {
                    MessageBox.Show("API không trả về sản phẩm!");
                    return;
                }

                if (product.Price <= 0)
                {
                    MessageBox.Show("Sản phẩm chưa có giá bán hợp lệ!");
                    return;
                }

                var existingItem = _cart.FirstOrDefault(
                    x => x.ProductId == product.ProductId);

                int nextQuantity =
                    (existingItem?.Quantity ?? 0) + 1;

                if (nextQuantity > product.StockQuantity)
                {
                    MessageBox.Show(
                        $"Không đủ tồn kho!\n"
                        + $"Sản phẩm: {product.ProductName}\n"
                        + $"Tồn kho hiện tại: {product.StockQuantity}");

                    return;
                }

                if (existingItem == null)
                {
                    _cart.Add(new PosCartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }
                else
                {
                    existingItem.Quantity++;
                }

                txtBarcode.Clear();
                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
                txtBarcode.Focus();
            }
        }

        // ================= TRA KHÁCH HÀNG =================

        private async void txtCustomerPhone_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            await FindCustomerAsync();
        }

        private async void btnFindCustomer_Click(
            object? sender,
            EventArgs e)
        {
            await FindCustomerAsync();
        }

        private void txtCustomerPhone_TextChanged(
            object? sender,
            EventArgs e)
        {
            lblCustomerName.Text =
                string.IsNullOrWhiteSpace(txtCustomerPhone.Text)
                    ? "Khách vãng lai"
                    : "Nhấn Tra cứu để xem khách hàng";
        }

        private async Task FindCustomerAsync()
        {
            if (_isBusy || !HasPosPermission())
                return;

            string phone = txtCustomerPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                lblCustomerName.Text = "Khách vãng lai";
                return;
            }

            SetBusy(true);

            try
            {
                // Dùng API search đang có trong bài.
                string endpoint =
                    "customers/search?keyword="
                    + Uri.EscapeDataString(phone);

                var customers = await ApiClientService.Client
                    .GetFromJsonAsync<List<PosCustomerDto>>(endpoint);

                // API tìm tương đối; POS lấy đúng số điện thoại.
                var customer = customers?.FirstOrDefault(
                    x => string.Equals(
                        x.PhoneNumber.Trim(),
                        phone,
                        StringComparison.OrdinalIgnoreCase));

                if (customer == null)
                {
                    lblCustomerName.Text =
                        "Chưa tìm thấy khách hàng có số điện thoại này";
                    return;
                }

                lblCustomerName.Text =
                    $"{customer.CustomerName}\n"
                    + $"Hạng: {customer.MembershipRank}"
                    + $" | Điểm: {customer.RewardPoints}";
            }
            catch (Exception ex)
            {
                lblCustomerName.Text = "Không tra được khách hàng";

                MessageBox.Show(
                    "Lỗi tra khách hàng: " + ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // ================= TÍNH TIỀN =================

        private decimal GetTotal()
        {
            return _cart.Sum(x => x.TotalPrice);
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString(
                "N0",
                CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private bool TryReadCash(out decimal cash)
        {
            // Nhập số nguyên đồng, ví dụ 100000.
            return decimal.TryParse(
                       txtCashReceived.Text.Trim(),
                       NumberStyles.None,
                       CultureInfo.InvariantCulture,
                       out cash)
                   && cash >= 0;
        }

        private void UpdateCartDisplay()
        {
            _cartSource.ResetBindings(false);
            lblTotalAmount.Text = FormatMoney(GetTotal());
            CalculateChange();
        }

        private void txtCashReceived_TextChanged(
            object? sender,
            EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            if (string.IsNullOrWhiteSpace(txtCashReceived.Text))
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
                return;
            }

            if (!TryReadCash(out decimal cash))
            {
                lblChange.Text = "Tiền không hợp lệ";
                lblChange.ForeColor = Color.Red;
                return;
            }

            decimal change = cash - GetTotal();

            if (change < 0)
            {
                lblChange.Text = "Chưa đủ tiền";
                lblChange.ForeColor = Color.Red;
            }
            else
            {
                lblChange.Text = FormatMoney(change);
                lblChange.ForeColor =
                    Color.FromArgb(22, 163, 74);
            }
        }

        // ================= XÓA / HỦY GIỎ =================

        private void btnRemoveItem_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy)
                return;

            if (dgvCart.CurrentRow?.DataBoundItem
                is not PosCartItem item)
            {
                MessageBox.Show("Hãy chọn dòng cần xóa!");
                return;
            }

            _cart.Remove(item);
            UpdateCartDisplay();
        }

        private void btnClearCart_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy)
                return;

            if (_cart.Count > 0)
            {
                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn hủy giỏ hàng?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            ResetSale();
            txtBarcode.Focus();
        }

        private void ResetSale()
        {
            _cart.Clear();

            txtBarcode.Clear();
            txtCashReceived.Clear();
            txtCustomerPhone.Clear();

            lblCustomerName.Text = "Khách vãng lai";
            UpdateCartDisplay();
        }

        // ================= THANH TOÁN =================

        private async void btnCheckout_Click(
            object? sender,
            EventArgs e)
        {
            await CheckoutAsync();
        }

        private async void FormPOS_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F9)
                return;

            e.SuppressKeyPress = true;
            await CheckoutAsync();
        }

        private async Task CheckoutAsync()
        {
            if (_isBusy || !HasPosPermission())
                return;

            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }

            if (!TryReadCash(out decimal cashReceived))
            {
                MessageBox.Show(
                    "Nhập tiền khách đưa bằng số nguyên đồng.\n"
                    + "Ví dụ: 100000");

                txtCashReceived.Focus();
                return;
            }

            decimal total = GetTotal();

            if (cashReceived < total)
            {
                MessageBox.Show("Tiền khách đưa chưa đủ!");
                txtCashReceived.Focus();
                return;
            }

            var confirm = MessageBox.Show(
                $"Tổng tiền: {FormatMoney(total)}\n"
                + $"Tiền khách đưa: {FormatMoney(cashReceived)}\n"
                + $"Tiền thừa: {FormatMoney(cashReceived - total)}\n\n"
                + "Xác nhận thanh toán?",
                "Thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            SetBusy(true);

            try
            {
                // Cấu trúc gửi theo hướng dẫn POS trong đề.
                var orderRequest = new
                {
                    CashierUsername =
                        SessionManager.CurrentUsername,

                    CustomerPhone =
                        txtCustomerPhone.Text.Trim(),

                    CashReceived = cashReceived,

                    Items = _cart.Select(item => new
                    {
                        item.ProductId,
                        item.Quantity,
                        item.UnitPrice
                    }).ToList()
                };

                using var response =
                    await ApiClientService.Client.PostAsJsonAsync(
                        "orders/checkout",
                        orderRequest);

                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync(
                        response,
                        "Thanh toán thất bại.");

                    // Giữ giỏ hàng khi API báo lỗi.
                    return;
                }

                MessageBox.Show(
                    "Thanh toán thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ResetSale();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Chưa nhận được xác nhận thanh toán từ máy chủ.\n"
                    + "Kiểm tra hóa đơn đã lưu trước khi thanh toán lại.\n\n"
                    + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
                txtBarcode.Focus();
            }
        }

        // ================= HÀM HỖ TRỢ =================

        private void SetBusy(bool busy)
        {
            _isBusy = busy;

            txtBarcode.Enabled = !busy;
            txtCustomerPhone.Enabled = !busy;
            txtCashReceived.Enabled = !busy;

            btnAddBarcode.Enabled = !busy;
            btnFindCustomer.Enabled = !busy;
            btnCheckout.Enabled = !busy;
            btnClearCart.Enabled = !busy;
            btnRemoveItem.Enabled = !busy;

            dgvCart.Enabled = !busy;
            UseWaitCursor = busy;
        }

        private static async Task ShowApiErrorAsync(
            HttpResponseMessage response,
            string title)
        {
            string detail =
                await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(detail))
            {
                detail = response.ReasonPhrase
                    ?? "Không có thông tin chi tiết.";
            }

            MessageBox.Show(
                $"{title}\n"
                + $"HTTP {(int)response.StatusCode}\n"
                + detail,
                "Lỗi API",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            _cartSource.Dispose();
            base.OnFormClosed(e);
        }
    }

    // Tên riêng để tránh trùng DTO đang có trong các Form khác.
    public class PosCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class PosProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }

    public class PosCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MembershipRank { get; set; } = string.Empty;
        public int RewardPoints { get; set; }
    }
}