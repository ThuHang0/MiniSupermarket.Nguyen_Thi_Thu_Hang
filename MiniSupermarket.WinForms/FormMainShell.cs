namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private Form? _activeForm;

        public FormMainShell()
        {
            InitializeComponent();
        }

        private void FormMainShell_Load(
            object? sender,
            EventArgs e)
        {
            string role = SessionManager.CurrentRole;

            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken) ||
                (role != "Admin" &&
                 role != "Cashier" &&
                 role != "Warehouse"))
            {
                MessageBox.Show(
                    "Phiên đăng nhập hoặc vai trò không hợp lệ.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            lblUserInfo.Text =
                $"Nhân viên: {SessionManager.CurrentFullName} " +
                $"({SessionManager.CurrentUsername}) | Vai trò: {role}";

            ApplyRolePermissions();
            OpenDefaultScreenByRole();
        }

        private void ApplyRolePermissions()
        {
            string role = SessionManager.CurrentRole;

            bool admin = role == "Admin";
            bool cashier = role == "Cashier";
            bool warehouse = role == "Warehouse";

            btnPOS.Visible = admin || cashier;
            btnCustomer.Visible = admin || cashier;

            btnCategory.Visible = admin || warehouse;
            btnProduct.Visible = admin || warehouse;

            btnReports.Visible = admin;
            btnUserManage.Visible = admin;
        }

        private bool CheckPermission(params string[] allowedRoles)
        {
            if (allowedRoles.Contains(SessionManager.CurrentRole))
            {
                return true;
            }

            MessageBox.Show(
                "Bạn không có quyền sử dụng chức năng này.",
                "Từ chối truy cập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private void OpenDefaultScreenByRole()
        {
            switch (SessionManager.CurrentRole)
            {
                case "Admin":
                    OpenChildForm(
                        new FormCategoryManagement(),
                        "QUẢN LÝ DANH MỤC",
                        btnCategory);
                    break;

                case "Cashier":
                    OpenChildForm(
                        new FormPOS(),
                        "BÁN HÀNG (POS)",
                        btnPOS);
                    break;

                case "Warehouse":
                    OpenChildForm(
                        new FormProductManagement(),
                        "QUẢN LÝ SẢN PHẨM",
                        btnProduct);
                    break;
            }
        }

        private void CloseActiveForm()
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            panelMainContent.Controls.Clear();
        }

        private void OpenChildForm(
            Form childForm,
            string title,
            Button activeButton)
        {
            CloseActiveForm();
            HighlightActiveButton(activeButton);

            _activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMainContent.Controls.Add(childForm);

            lblTitle.Text = title;

            childForm.Show();
            childForm.BringToFront();
        }

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control control in panelMenu.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor =
                        Color.FromArgb(24, 30, 48);
                }
            }

            activeButton.BackColor =
                Color.FromArgb(41, 100, 180);
        }

        // Màn hình tạm cho những Form sẽ viết ở phần tiếp theo.
        private void ShowPendingScreen(
            string title,
            Button activeButton)
        {
            CloseActiveForm();
            HighlightActiveButton(activeButton);

            lblTitle.Text = title;

            var label = new Label
            {
                Dock = DockStyle.Fill,
                Text = title +
                    Environment.NewLine +
                    "Chức năng sẽ được triển khai ở phần tiếp theo.",
                Font = new Font("Segoe UI", 16F),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelMainContent.Controls.Add(label);
        }

        private void btnCategory_Click(
            object? sender,
            EventArgs e)
        {
            if (!CheckPermission("Admin", "Warehouse"))
                return;

            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC",
                btnCategory);
        }

        private void btnCustomer_Click(
            object? sender,
            EventArgs e)
        {
            if (!CheckPermission("Admin", "Cashier"))
                return;

            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG",
                btnCustomer);
        }

        private void btnPOS_Click(
            object? sender,
            EventArgs e)
                {
                    if (!CheckPermission("Admin", "Cashier"))
                        return;

                    OpenChildForm(
                        new FormPOS(),
                        "BÁN HÀNG (POS)",
                        btnPOS);
        }

        private void btnProduct_Click(
          object? sender,
          EventArgs e)
            {
                if (!CheckPermission("Admin", "Warehouse"))
                    return;

                OpenChildForm(
                    new FormProductManagement(),
                    "QUẢN LÝ SẢN PHẨM",
                    btnProduct);
        }

        private void btnReports_Click(
            object? sender,
            EventArgs e)
                {
                    if (!CheckPermission("Admin"))
                    {
                        return;
                    }

                    OpenChildForm(
                        new FormQuickReport(),
                        "BÁO CÁO DOANH THU",
                        btnReports);
        }

        private void btnUserManage_Click(
            object? sender,
            EventArgs e)
        {
            if (!CheckPermission("Admin"))
            {
                return;
            }

            OpenChildForm(
                new FormUserManagement(),
                "QUẢN TRỊ TÀI KHOẢN",
                btnUserManage);
        }

        private void btnLogout_Click(
            object? sender,
            EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}