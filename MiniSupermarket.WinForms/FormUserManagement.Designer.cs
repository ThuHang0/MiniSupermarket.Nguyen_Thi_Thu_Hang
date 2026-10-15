using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer? components = null;

        private DataGridView dgvUsers = null!;

        private TextBox txtUsername = null!;
        private TextBox txtFullName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;

        private ComboBox cboRole = null!;
        private Label lblSelectedUser = null!;

        private Button btnAddUser = null!;
        private Button btnResetPassword = null!;
        private Button btnToggleLock = null!;
        private Button btnNew = null!;
        private Button btnLoad = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();

            txtUsername = new TextBox();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();

            cboRole = new ComboBox();
            lblSelectedUser = new Label();

            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();
            btnNew = new Button();
            btnLoad = new Button();

            SuspendLayout();

            // VÙNG NHẬP THÔNG TIN
            var details = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 300,
                Padding = new Padding(12),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White
            };

            lblSelectedUser.Text = "Đang nhập tài khoản mới";
            lblSelectedUser.Size = new Size(250, 48);
            lblSelectedUser.Font = new System.Drawing.Font(
                "Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblSelectedUser.ForeColor = Color.SteelBlue;

            details.Controls.Add(lblSelectedUser);

            txtUsername.Name = "txtUsername";
            txtUsername.MaxLength = 50;

            txtFullName.Name = "txtFullName";
            txtFullName.MaxLength = 100;

            txtEmail.Name = "txtEmail";
            txtEmail.MaxLength = 150;

            txtPassword.Name = "txtPassword";
            txtPassword.MaxLength = 128;
            txtPassword.UseSystemPasswordChar = true;

            cboRole.Name = "cboRole";
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Items.AddRange(new object[]
            {
                "Admin",
                "Cashier",
                "Warehouse"
            });
            cboRole.SelectedItem = "Cashier";

            AddField(details, "Tên đăng nhập", txtUsername);
            AddField(details, "Họ và tên", txtFullName);
            AddField(details, "Email", txtEmail);
            AddField(details, "Mật khẩu mới", txtPassword);
            AddField(details, "Vai trò khi tạo mới", cboRole);

            AddButton(
                details, btnAddUser,
                "Thêm tài khoản", Color.SeaGreen);

            AddButton(
                details, btnResetPassword,
                "Đặt lại mật khẩu", Color.SteelBlue);

            AddButton(
                details, btnToggleLock,
                "Khóa / Mở khóa", Color.Firebrick);

            AddButton(
                details, btnNew,
                "Nhập tài khoản mới", Color.DimGray);

            AddButton(
                details, btnLoad,
                "Tải lại danh sách", Color.DimGray);

            // DANH SÁCH TÀI KHOẢN
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.MultiSelect = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            AddColumn("Username", "Tên đăng nhập", 110);
            AddColumn("FullName", "Họ tên", 140);
            AddColumn("Email", "Email", 140);
            AddColumn("Role", "Vai trò", 90);

            dgvUsers.Columns.Add(
                new DataGridViewCheckBoxColumn
                {
                    Name = "IsActive",
                    DataPropertyName = "IsActive",
                    HeaderText = "Hoạt động",
                    FillWeight = 70
                });

            dgvUsers.CellClick += dgvUsers_CellClick;

            btnAddUser.Click += btnAddUser_Click;
            btnResetPassword.Click += btnResetPassword_Click;
            btnToggleLock.Click += btnToggleLock_Click;
            btnNew.Click += btnNew_Click;
            btnLoad.Click += btnLoad_Click;

            Controls.Add(dgvUsers);
            Controls.Add(details);

            Name = "FormUserManagement";
            Text = "Quản trị tài khoản";
            ClientSize = new Size(1020, 600);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);

            Load += FormUserManagement_Load;

            ResumeLayout(false);
        }

        private void AddColumn(
            string property,
            string title,
            float weight)
        {
            dgvUsers.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = property,
                    DataPropertyName = property,
                    HeaderText = title,
                    FillWeight = weight
                });
        }

        private void AddField(
            FlowLayoutPanel panel,
            string title,
            Control input)
        {
            panel.Controls.Add(new Label
            {
                Text = title,
                AutoSize = true,
                Margin = new Padding(3, 8, 3, 3)
            });

            input.Width = 250;
            panel.Controls.Add(input);
        }

        private void AddButton(
            FlowLayoutPanel panel,
            Button button,
            string title,
            Color background)
        {
            button.Text = title;
            button.Size = new Size(250, 36);
            button.Margin = new Padding(3, 8, 3, 0);
            button.BackColor = background;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;

            panel.Controls.Add(button);
        }
    }
}