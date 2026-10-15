namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer? components = null;

        private Panel panelSidebar = null!;
        private Panel panelTopHeader = null!;
        private Panel panelMainContent = null!;
        private Panel panelUserFooter = null!;

        private FlowLayoutPanel panelMenu = null!;

        private Label lblLogo = null!;
        private Label lblTitle = null!;
        private Label lblUserInfo = null!;

        private Button btnPOS = null!;
        private Button btnCategory = null!;
        private Button btnProduct = null!;
        private Button btnCustomer = null!;
        private Button btnReports = null!;
        private Button btnUserManage = null!;
        private Button btnLogout = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            panelTopHeader = new Panel();
            panelMainContent = new Panel();
            panelUserFooter = new Panel();

            panelMenu = new FlowLayoutPanel();

            lblLogo = new Label();
            lblTitle = new Label();
            lblUserInfo = new Label();

            btnPOS = new Button();
            btnCategory = new Button();
            btnProduct = new Button();
            btnCustomer = new Button();
            btnReports = new Button();
            btnUserManage = new Button();
            btnLogout = new Button();

            SuspendLayout();

            // SIDEBAR
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 230;
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);

            // LOGO
            lblLogo.Name = "lblLogo";
            lblLogo.Text = "MiniMart POS";
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Height = 85;
            lblLogo.ForeColor = Color.White;
            lblLogo.Font = new Font(
                "Segoe UI", 19F, FontStyle.Bold);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // VÙNG MENU
            panelMenu.Name = "panelMenu";
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.FlowDirection = FlowDirection.TopDown;
            panelMenu.WrapContents = false;
            panelMenu.AutoScroll = true;
            panelMenu.Padding = new Padding(8, 10, 8, 0);
            panelMenu.BackColor = Color.FromArgb(24, 30, 48);

            ConfigureMenuButton(
                btnPOS, "btnPOS", "Bán hàng (POS)");
            ConfigureMenuButton(
                btnCategory, "btnCategory", "Quản lý Danh mục");
            ConfigureMenuButton(
                btnProduct, "btnProduct", "Quản lý Sản phẩm");
            ConfigureMenuButton(
                btnCustomer, "btnCustomer", "Quản lý Khách hàng");
            ConfigureMenuButton(
                btnReports, "btnReports", "Báo cáo Doanh thu");
            ConfigureMenuButton(
                btnUserManage, "btnUserManage", "Quản trị Tài khoản");

            btnPOS.Click += btnPOS_Click;
            btnCategory.Click += btnCategory_Click;
            btnProduct.Click += btnProduct_Click;
            btnCustomer.Click += btnCustomer_Click;
            btnReports.Click += btnReports_Click;
            btnUserManage.Click += btnUserManage_Click;

            panelMenu.Controls.AddRange(new Control[]
            {
                btnPOS,
                btnCategory,
                btnProduct,
                btnCustomer,
                btnReports,
                btnUserManage
            });

            // VÙNG ĐĂNG XUẤT
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Height = 75;
            panelUserFooter.Padding = new Padding(12);

            btnLogout.Name = "btnLogout";
            btnLogout.Text = "Đăng xuất";
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = Color.FromArgb(180, 55, 65);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font(
                "Segoe UI", 11F, FontStyle.Bold);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += btnLogout_Click;

            panelUserFooter.Controls.Add(btnLogout);

            panelSidebar.Controls.Add(panelMenu);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Controls.Add(lblLogo);

            // HEADER
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Height = 90;
            panelTopHeader.BackColor = Color.White;

            lblTitle.Name = "lblTitle";
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Padding = new Padding(20, 0, 10, 0);
            lblTitle.Font = new Font(
                "Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(24, 30, 48);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Dock = DockStyle.Bottom;
            lblUserInfo.Height = 30;
            lblUserInfo.Padding = new Padding(20, 0, 10, 0);
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.DimGray;
            lblUserInfo.TextAlign = ContentAlignment.MiddleLeft;

            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Controls.Add(lblUserInfo);

            // VÙNG NỘI DUNG
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.BackColor =
                Color.FromArgb(244, 245, 247);
            panelMainContent.Padding = new Padding(12);

            // FORM CHÍNH
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            MinimumSize = new Size(1100, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F);
            Text = "Hệ thống Quản lý Bán lẻ và Tồn kho Siêu thị Mini";
            Name = "FormMainShell";

            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);

            Load += FormMainShell_Load;

            ResumeLayout(false);
        }

        private void ConfigureMenuButton(
            Button button,
            string name,
            string text)
        {
            button.Name = name;
            button.Text = text;
            button.Size = new Size(210, 48);
            button.Margin = new Padding(0, 0, 0, 6);
            button.Padding = new Padding(12, 0, 0, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(24, 30, 48);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Cursor = Cursors.Hand;
        }
    }
}