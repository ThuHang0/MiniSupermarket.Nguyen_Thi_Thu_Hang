
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer? components = null;

        private Panel panelSidebar = null!;
        private Panel panelTopHeader = null!;
        private Panel panelMainContent = null!;
        private Panel panelUserFooter = null!;
        private Panel panelMenu = null!;

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
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            panelMenu = new Panel();
            btnUserManage = new Button();
            btnReports = new Button();
            btnCustomer = new Button();
            btnProduct = new Button();
            btnCategory = new Button();
            btnPOS = new Button();
            lblLogo = new Label();
            panelUserFooter = new Panel();
            btnLogout = new Button();
            panelTopHeader = new Panel();
            lblUserInfo = new Label();
            lblTitle = new Label();
            panelMainContent = new Panel();
            panelSidebar.SuspendLayout();
            panelMenu.SuspendLayout();
            panelUserFooter.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            panelSidebar.Controls.Add(panelMenu);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(230, 720);
            panelSidebar.TabIndex = 2;
            // 
            // panelMenu
            // 
            panelMenu.AutoScroll = true;
            panelMenu.BackColor = Color.FromArgb(24, 30, 48);
            panelMenu.Controls.Add(btnUserManage);
            panelMenu.Controls.Add(btnReports);
            panelMenu.Controls.Add(btnCustomer);
            panelMenu.Controls.Add(btnProduct);
            panelMenu.Controls.Add(btnCategory);
            panelMenu.Controls.Add(btnPOS);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Location = new Point(0, 85);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(230, 565);
            panelMenu.TabIndex = 0;
            // 
            // btnUserManage
            // 
            btnUserManage.Location = new Point(0, 0);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Size = new Size(75, 23);
            btnUserManage.TabIndex = 0;
            // 
            // btnReports
            // 
            btnReports.Location = new Point(0, 0);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(75, 23);
            btnReports.TabIndex = 1;
            // 
            // btnCustomer
            // 
            btnCustomer.Location = new Point(0, 0);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(75, 23);
            btnCustomer.TabIndex = 2;
            // 
            // btnProduct
            // 
            btnProduct.Location = new Point(0, 0);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(75, 23);
            btnProduct.TabIndex = 3;
            // 
            // btnCategory
            // 
            btnCategory.Location = new Point(0, 0);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(75, 23);
            btnCategory.TabIndex = 4;
            // 
            // btnPOS
            // 
            btnPOS.Location = new Point(0, 0);
            btnPOS.Name = "btnPOS";
            btnPOS.Size = new Size(75, 23);
            btnPOS.TabIndex = 5;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(230, 85);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "\U0001f6d2 MINIMART POS";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUserFooter
            // 
            panelUserFooter.BackColor = Color.FromArgb(19, 24, 39);
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 650);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(230, 70);
            panelUserFooter.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(19, 24, 39);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(230, 70);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪  Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // panelTopHeader
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(230, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Padding = new Padding(20, 0, 20, 0);
            panelTopHeader.Size = new Size(1050, 65);
            panelTopHeader.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            lblUserInfo.Dock = DockStyle.Fill;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.FromArgb(71, 85, 105);
            lblUserInfo.Location = new Point(480, 0);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(550, 65);
            lblUserInfo.TabIndex = 0;
            lblUserInfo.Text = "Xin chào: ...";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(20, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 65);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(230, 65);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Padding = new Padding(15);
            panelMainContent.Size = new Size(1050, 655);
            panelMainContent.TabIndex = 0;
            panelMainContent.Paint += panelMainContent_Paint;
            // 
            // FormMainShell
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 650);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Tiệm Tạp Hóa";
            panelSidebar.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelUserFooter.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Hàm tạo kiểu chung cho Button menu
        private void SetupMenuButton(
            Button btn,
            string name,
            string text)
        {
            btn.Name = name;
            btn.Text = text;

            btn.Dock = DockStyle.Top;
            btn.Height = 55;

            btn.BackColor =
                Color.FromArgb(24, 30, 48);

            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.TextAlign =
                ContentAlignment.MiddleLeft;

            btn.Padding = new Padding(18, 0, 0, 0);

            btn.Font = new Font(
                "Segoe UI", 10F, FontStyle.Regular);

            btn.Cursor = Cursors.Hand;

            btn.UseVisualStyleBackColor = false;
        }
    }
}
