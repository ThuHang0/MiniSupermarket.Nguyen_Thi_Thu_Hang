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

        public FormCustomerManagement()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7063/api/")
            };
        }

        private async void FormCustomerManagement_Load(object sender, EventArgs e)
        {
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

        // 2. Nút Thêm mới (POST)
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newCustomer = new Customer
            {
                CustomerName = txtCustomerName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Address = txtAddress.Text,
                RewardPoints = int.TryParse(txtRewardPoints.Text, out int points) ? points : 0,
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text
            };

            var response = await _httpClient.PostAsJsonAsync("customers", newCustomer);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                await LoadCustomers();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại thông tin!");
            }
        }

        // 3. Nút Cập nhật (PUT)
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa từ danh sách!");
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            var updatedCustomer = new Customer
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Address = txtAddress.Text,
                RewardPoints = int.TryParse(txtRewardPoints.Text, out int points) ? points : 0,
                MembershipRank = txtMembershipRank.Text
            };

            var response = await _httpClient.PutAsJsonAsync($"customers/{id}", updatedCustomer);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                await LoadCustomers();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        // 4. Nút Xóa (DELETE)
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?",
                                     "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                int id = int.Parse(txtCustomerId.Text);
                var response = await _httpClient.DeleteAsync($"customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa khách hàng thành công!");
                    await LoadCustomers();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
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