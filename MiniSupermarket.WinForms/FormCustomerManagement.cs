using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // Đổi cổng 7063 thành cổng thực tế mà Swagger Backend của bạn đang chạy
        private readonly HttpClient _httpClient;

        //public FormCustomerManagement()
        //{
        //    InitializeComponent();
        //    _httpClient = new HttpClient
        //    {
        //        BaseAddress = new Uri("https://localhost:7063/api/")
        //    };
        //}
        public FormCustomerManagement()
        {
            InitializeComponent();

            _httpClient = ApiClientService.Client;
        }

        private async void FormCustomerManagement_Load(
            object sender,
            EventArgs e)
        {
            bool isAdmin = SessionManager.CurrentRole == "Admin";

            txtRewardPoints.ReadOnly = !isAdmin;
            txtMembershipRank.ReadOnly = !isAdmin;

            await LoadCustomers();
        }

        // 1. Nút Tải lại danh sách
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        // Hàm gọi API GET /api/customers
        private async Task LoadCustomers()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("customers");
                dgvCustomers.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task SaveCustomerAsync(
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
                        "Không thể lưu khách hàng",
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
                await LoadCustomers();
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
        private Customer? ReadCustomerInputs()
        {
            string name = txtCustomerName.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();
            string rank = txtMembershipRank.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                txtCustomerName.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.");
                txtPhoneNumber.Focus();
                return null;
            }

            if (!int.TryParse(txtRewardPoints.Text, out int points) ||
                points < 0)
            {
                MessageBox.Show("Điểm thưởng phải là số nguyên không âm.");
                txtRewardPoints.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(rank))
            {
                MessageBox.Show("Vui lòng nhập hạng khách hàng.");
                txtMembershipRank.Focus();
                return null;
            }

            return new Customer
            {
                CustomerName = name,
                PhoneNumber = phone,
                Address = txtAddress.Text.Trim(),
                RewardPoints = points,
                MembershipRank = rank
            };
        }

        // 2. Nút Thêm mới (POST)
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var customer = ReadCustomerInputs();

            if (customer == null)
                return;

            if (SessionManager.CurrentRole == "Cashier")
            {
                customer.RewardPoints = 0;
                customer.MembershipRank = "Chuẩn";
            }

            await SaveCustomerAsync(
                () => ApiClientService.Client.PostAsJsonAsync(
                    "customers",
                    customer),
                "Thêm khách hàng thành công!");
        }

        // 3. Nút Cập nhật (PUT)
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCustomerId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.");
                return;
            }

            var customer = ReadCustomerInputs();

            if (customer == null)
                return;

            customer.CustomerId = id;

            await SaveCustomerAsync(
                () => ApiClientService.Client.PutAsJsonAsync(
                    $"customers/{id}",
                    customer),
                "Cập nhật khách hàng thành công!");
        }

        // 4. Nút Xóa (DELETE)
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCustomerId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa khách hàng này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            try
            {
                using var response = await ApiClientService.Client
                    .DeleteAsync($"customers/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    string message =
                        await ApiClientService.ReadErrorMessageAsync(response);

                    MessageBox.Show(
                        message,
                        "Không thể xóa khách hàng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                ClearInputs();

                MessageBox.Show(
                    "Xóa khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadCustomers();
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

        // 5. Nút Tìm kiếm (GET Search)
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadCustomers(); // Trống thì tải lại toàn bộ
                return;
            }

            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Customer>>($"customers/search?keyword={keyword}");
                dgvCustomers.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // 6. Đổ dữ liệu từ GridView ngược lên TextBox khi click vào 1 dòng
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                txtCustomerId.Text = row.Cells["CustomerId"].Value?.ToString();
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString();
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString();
                txtMembershipRank.Text = row.Cells["MembershipRank"].Value?.ToString();
            }
        }

        // Hàm hỗ trợ xóa trắng form
        private void ClearInputs()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Clear();
            txtMembershipRank.Clear();
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // Lớp DTO để hứng dữ liệu trả về từ API
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; }
    }
}