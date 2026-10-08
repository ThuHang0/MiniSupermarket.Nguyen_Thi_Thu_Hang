
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        // Lưu Form đang hiển thị
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
            SetupMenuButton(btnPOS, "btnPOS", "Bán hàng (POS)");
            SetupMenuButton(btnCategory, "btnCategory", "Quản lý Danh mục");
            SetupMenuButton(btnProduct, "btnProduct", "Quản lý Sản phẩm");
            SetupMenuButton(btnCustomer, "btnCustomer", "Quản lý Khách hàng");
            SetupMenuButton(btnReports, "btnReports", "Báo cáo Doanh thu");
            SetupMenuButton(btnUserManage, "btnUserManage", "Quản trị Tài khoản");

            // Đăng ký sự kiện
            Load += FormMainShell_Load;

            btnPOS.Click += btnPOS_Click;
            btnCategory.Click += btnCategory_Click;
            btnProduct.Click += btnProduct_Click;
            btnCustomer.Click += btnCustomer_Click;
            btnReports.Click += btnReports_Click;
            btnUserManage.Click += btnUserManage_Click;
            btnLogout.Click += btnLogout_Click;
        }

        // ================= FORM LOAD =================

        private void FormMainShell_Load(
            object? sender,
            EventArgs e)
        {
            lblUserInfo.Text =
                $"Nhân viên: {SessionManager.CurrentUsername}" +
                $" | Vai trò: {SessionManager.CurrentRole}";

            ApplyRolePermissions(
                SessionManager.CurrentRole);

            OpenDefaultScreenByRole(
                SessionManager.CurrentRole);
        }

        // ================= OPEN CHILD FORM =================

        private void OpenChildForm(
            Form childForm,
            string screenTitle,
            Button senderButton)
        {
            // Đóng Form cũ
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            // Lưu Form mới
            _activeForm = childForm;

            // Bỏ chế độ cửa sổ độc lập
            childForm.TopLevel = false;

            // Không hiển thị thanh tiêu đề Form
            childForm.FormBorderStyle =
                FormBorderStyle.None;

            // Cho Form chiếm toàn bộ vùng nội dung
            childForm.Dock = DockStyle.Fill;

            // Xóa nội dung cũ
            panelMainContent.Controls.Clear();

            // Thêm Form mới
            panelMainContent.Controls.Add(childForm);

            panelMainContent.Tag = childForm;

            // Cập nhật tiêu đề
            lblTitle.Text = screenTitle;

            // Làm nổi bật nút
            HighlightActiveButton(senderButton);

            // Hiển thị Form
            childForm.BringToFront();
            childForm.Show();
        }

        // ================= HIGHLIGHT BUTTON =================

        private void HighlightActiveButton(
            Button activeButton)
        {
            foreach (Control control
                in panelMenu.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor =
                        Color.FromArgb(24, 30, 48);

                    btn.Font = new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Regular);
                }
            }

            activeButton.BackColor =
                Color.FromArgb(41, 100, 180);

            activeButton.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);
        }

        // ================= ROLE PERMISSIONS =================

        private void ApplyRolePermissions(
            string role)
        {
            // Mặc định ẩn tất cả chức năng
            btnPOS.Visible = false;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnCustomer.Visible = false;
            btnReports.Visible = false;
            btnUserManage.Visible = false;

            switch (role.ToUpperInvariant())
            {
                case "ADMIN":
                    // Quản trị viên toàn quyền
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    break;

                case "CASHIER":
                    // Thu ngân
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    break;

                case "WAREHOUSE":
                    // Thủ kho
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    break;

                default:
                    MessageBox.Show(
                        "Vai trò người dùng không hợp lệ!",
                        "Lỗi phân quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    this.Close();
                    break;
            }
        }

        // ================= CHECK ROLE =================

        private bool HasPermission(
            params string[] allowedRoles)
        {
            foreach (string role in allowedRoles)
            {
                if (string.Equals(
                    SessionManager.CurrentRole,
                    role,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            MessageBox.Show(
                "Bạn không có quyền truy cập chức năng này!",
                "Từ chối truy cập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);

            return false;
        }

        // ================= DEFAULT SCREEN =================

        private void OpenDefaultScreenByRole(
            string role)
        {
            switch (role.ToUpperInvariant())
            {
                case "ADMIN":
                case "WAREHOUSE":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC HÀNG HÓA",
                        btnCategory);
                    break;

                case "CASHIER":
                    OpenChildForm(
                        new FormCustomerManagement(),
                        "QUẢN LÝ KHÁCH HÀNG",
                        btnCustomer);
                    break;
            }
        }

        // ================= CATEGORY =================

        private void btnCategory_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN", "WAREHOUSE"))
                return;

            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC HÀNG HÓA",
                btnCategory);
        }

        // ================= CUSTOMER =================

        private void btnCustomer_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN", "CASHIER"))
                return;

            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG THÀNH VIÊN",
                btnCustomer);
        }

        // ================= POS =================

        private void btnPOS_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN", "CASHIER"))
                return;

            OpenChildForm(
                new FormPOS(),
                "BÁN HÀNG POS",
                btnPOS);
        }

        // ================= PRODUCT =================

        //private void btnProduct_Click(
        //    object? sender,
        //    EventArgs e)
        //{
        //    if (!HasPermission("ADMIN", "WAREHOUSE"))
        //        return;

        //    MessageBox.Show(
        //        "Chức năng quản lý sản phẩm sẽ được bổ sung!",
        //        "Thông báo",
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Information);
        //}
        private void btnProduct_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN", "WAREHOUSE"))
                return;

            OpenChildForm(
                new FormProductManagement(),
                "QUẢN LÝ SẢN PHẨM VÀ TỒN KHO",
                btnProduct);
        }


        // ================= REPORTS =================

        private void btnReports_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN"))
                return;

            MessageBox.Show(
                "Chức năng báo cáo doanh thu sẽ được bổ sung!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ================= USER MANAGEMENT =================

        private void btnUserManage_Click(
            object? sender,
            EventArgs e)
        {
            if (!HasPermission("ADMIN"))
                return;

            MessageBox.Show(
                "Chức năng quản trị tài khoản sẽ được bổ sung!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ================= LOGOUT =================

        private void btnLogout_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Xóa phiên đăng nhập
                SessionManager.JwtToken = string.Empty;
                SessionManager.CurrentRole = string.Empty;
                SessionManager.CurrentUsername = string.Empty;

                // Trả về màn hình đăng nhập
                this.Close();
            }
        }

        private void panelMainContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
