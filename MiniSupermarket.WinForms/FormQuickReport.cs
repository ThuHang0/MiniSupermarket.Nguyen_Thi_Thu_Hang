using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private bool _isLoading;

        public FormQuickReport()
        {
            InitializeComponent();
        }

        private void FormQuickReport_Load(
            object? sender,
            EventArgs e)
        {
            if (!HasReportPermission())
            {
                Close();
                return;
            }

            dtpReportDate.Value = DateTime.Today;
        }

        private bool HasReportPermission()
        {
            if (SessionManager.CurrentRole == "Admin")
            {
                return true;
            }

            MessageBox.Show(
                "Bạn không có quyền xem doanh thu.",
                "Từ chối truy cập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private async void btnRunReport_Click(
            object? sender,
            EventArgs e)
        {
            // Kiểm tra quyền ngay tại hành động xem báo cáo.
            if (_isLoading || !HasReportPermission())
            {
                return;
            }

            DateTime selectedDate = dtpReportDate.Value.Date;

            _isLoading = true;
            btnRunReport.Enabled = false;
            dtpReportDate.Enabled = false;
            UseWaitCursor = true;

            lblTotalOrders.Text = "—";
            lblTotalRevenue.Text = "—";
            lblBestSeller.Text = "—";
            lblReportStatus.Text = "Đang tải báo cáo...";

            try
            {
                string date = selectedDate.ToString(
                    "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture);

                using var response =
                    await ApiClientService.Client.GetAsync(
                        $"reports/quick?date={date}");

                if (!response.IsSuccessStatusCode)
                {
                    string message =
                        await ReadErrorMessageAsync(response);

                    throw new InvalidOperationException(message);
                }

                var report = await response.Content
                    .ReadFromJsonAsync<QuickReportDto>();

                if (report == null)
                {
                    throw new InvalidOperationException(
                        "API không trả về dữ liệu báo cáo.");
                }

                if (IsDisposed)
                {
                    return;
                }

                lblTotalOrders.Text =
                    report.TotalOrders.ToString("N0");

                lblTotalRevenue.Text =
                    $"{report.TotalRevenue:N2} đ";

                lblBestSeller.Text =
                    report.TotalOrders == 0
                        ? "Chưa có sản phẩm bán ra"
                        : $"{report.BestSeller}\n" +
                          $"Đã bán: {report.BestSellerQuantity:N0}";

                lblReportStatus.Text =
                    $"Báo cáo ngày {selectedDate:dd/MM/yyyy}. " +
                    "Dữ liệu lấy từ hóa đơn đã lưu trong SQL Server.";
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    lblReportStatus.Text = "Không tải được báo cáo.";

                    MessageBox.Show(
                        ex.Message,
                        "Lỗi báo cáo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isLoading = false;

                if (!IsDisposed)
                {
                    btnRunReport.Enabled = true;
                    dtpReportDate.Enabled = true;
                    UseWaitCursor = false;
                }
            }
        }

        private static async Task<string> ReadErrorMessageAsync(
            HttpResponseMessage response)
        {
            if (response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                return "Phiên đăng nhập đã hết hạn. " +
                       "Hãy đăng xuất và đăng nhập lại.";
            }

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Forbidden)
            {
                return "Tài khoản không có quyền xem báo cáo.";
            }

            string body =
                await response.Content.ReadAsStringAsync();

            try
            {
                using var document = JsonDocument.Parse(body);

                if (document.RootElement.TryGetProperty(
                    "message", out var message))
                {
                    return message.GetString()
                        ?? "Không tải được báo cáo.";
                }
            }
            catch (JsonException)
            {
            }

            return $"Không tải được báo cáo. " +
                   $"HTTP {(int)response.StatusCode}.";
        }
    }

    public class QuickReportDto
    {
        public DateTime ReportDate { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public string BestSeller { get; set; } = string.Empty;

        public long BestSellerQuantity { get; set; }
    }
}