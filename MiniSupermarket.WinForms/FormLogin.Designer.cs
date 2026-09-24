namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel pnlMain;
        private Panel pnlLogin;

        private Label lblLogo;
        private Label lblTitle;
        private Label lblSubtitle;

        private Label lblUsername;
        private Label lblPassword;

        private TextBox txtUser;
        private TextBox txtPass;

        private Button btnLogin;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlLogin = new Panel();
            lblLogo = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblUsername = new Label();
            txtUser = new TextBox();
            lblPassword = new Label();
            txtPass = new TextBox();
            btnLogin = new Button();
            pnlMain.SuspendLayout();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(245, 247, 250);
            pnlMain.Controls.Add(pnlLogin);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(900, 550);
            pnlMain.TabIndex = 0;
            pnlMain.Paint += pnlMain_Paint;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.White;
            pnlLogin.BorderStyle = BorderStyle.FixedSingle;
            pnlLogin.Controls.Add(lblLogo);
            pnlLogin.Controls.Add(lblTitle);
            pnlLogin.Controls.Add(lblSubtitle);
            pnlLogin.Controls.Add(lblUsername);
            pnlLogin.Controls.Add(txtUser);
            pnlLogin.Controls.Add(lblPassword);
            pnlLogin.Controls.Add(txtPass);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Location = new Point(275, 65);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(350, 420);
            pnlLogin.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(34, 139, 74);
            lblLogo.Location = new Point(143, 25);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(79, 54);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "\U0001f6d2";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 45, 55);
            lblTitle.Location = new Point(82, 88);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "MINI MARKET";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(79, 130);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(174, 17);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Đăng nhập hệ thống quản lý";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(50, 55, 60);
            lblUsername.Location = new Point(40, 175);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(73, 19);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Tài khoản";
            // 
            // txtUser
            // 
            txtUser.BackColor = Color.FromArgb(248, 249, 250);
            txtUser.BorderStyle = BorderStyle.FixedSingle;
            txtUser.Font = new Font("Segoe UI", 11F);
            txtUser.ForeColor = Color.FromArgb(40, 40, 40);
            txtUser.Location = new Point(40, 200);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Nhập tài khoản";
            txtUser.Size = new Size(268, 27);
            txtUser.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(50, 55, 60);
            lblPassword.Location = new Point(40, 245);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(71, 19);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtPass
            // 
            txtPass.BackColor = Color.FromArgb(248, 249, 250);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.ForeColor = Color.FromArgb(40, 40, 40);
            txtPass.Location = new Point(40, 270);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Nhập mật khẩu";
            txtPass.Size = new Size(268, 27);
            txtPass.TabIndex = 6;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(34, 139, 74);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 325);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(268, 45);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(900, 550);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mini Supermarket - Đăng nhập";
            pnlMain.ResumeLayout(false);
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
