namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvCustomers = new DataGridView();
            txtCustomerId = new TextBox();
            txtCustomerName = new TextBox();
            txtPhoneNumber = new TextBox();
            txtAddress = new TextBox();
            txtRewardPoints = new TextBox();
            txtMembershipRank = new TextBox();
            txtSearchKeyword = new TextBox();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            lblId = new Label();
            lblName = new Label();
            lblPhone = new Label();
            lblAddress = new Label();
            lblPoints = new Label();
            lblRank = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(10, 272);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.RowTemplate.Height = 24;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(838, 282);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellContentClick += dgvCustomers_CellContentClick;
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(116, 35);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(230, 23);
            txtCustomerId.TabIndex = 1;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(116, 74);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(230, 23);
            txtCustomerName.TabIndex = 3;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(116, 113);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(230, 23);
            txtPhoneNumber.TabIndex = 5;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(510, 35);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(300, 23);
            txtAddress.TabIndex = 7;
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Location = new Point(510, 74);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(300, 23);
            txtRewardPoints.TabIndex = 9;
            // 
            // txtMembershipRank
            // 
            txtMembershipRank.Location = new Point(510, 113);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(300, 23);
            txtMembershipRank.TabIndex = 11;
            // 
            // txtSearchKeyword
            // 
            txtSearchKeyword.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchKeyword.Location = new Point(528, 210);
            txtSearchKeyword.Name = "txtSearchKeyword";
            txtSearchKeyword.Size = new Size(219, 23);
            txtSearchKeyword.TabIndex = 6;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(10, 202);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(105, 42);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(121, 202);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 42);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(231, 202);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(105, 42);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(341, 202);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 42);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(752, 208);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(96, 30);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(23, 38);
            lblId.Name = "lblId";
            lblId.Size = new Size(92, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Mã khách hàng:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(23, 77);
            lblName.Name = "lblName";
            lblName.Size = new Size(47, 15);
            lblName.TabIndex = 2;
            lblName.Text = "Họ Tên:";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(23, 116);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(79, 15);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Số điện thoại:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(433, 38);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(46, 15);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Địa chỉ:";
            // 
            // lblPoints
            // 
            lblPoints.AutoSize = true;
            lblPoints.Location = new Point(433, 77);
            lblPoints.Name = "lblPoints";
            lblPoints.Size = new Size(80, 15);
            lblPoints.TabIndex = 8;
            lblPoints.Text = "Điểm thưởng:";
            // 
            // lblRank
            // 
            lblRank.AutoSize = true;
            lblRank.Location = new Point(433, 116);
            lblRank.Name = "lblRank";
            lblRank.Size = new Size(59, 15);
            lblRank.TabIndex = 10;
            lblRank.Text = "Hạng thẻ:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtMembershipRank);
            groupBox1.Controls.Add(lblRank);
            groupBox1.Controls.Add(txtRewardPoints);
            groupBox1.Controls.Add(lblPoints);
            groupBox1.Controls.Add(txtAddress);
            groupBox1.Controls.Add(lblAddress);
            groupBox1.Controls.Add(txtPhoneNumber);
            groupBox1.Controls.Add(lblPhone);
            groupBox1.Controls.Add(txtCustomerName);
            groupBox1.Controls.Add(lblName);
            groupBox1.Controls.Add(txtCustomerId);
            groupBox1.Controls.Add(lblId);
            groupBox1.Location = new Point(10, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(838, 161);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin Khách hàng";
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 565);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchKeyword);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(btnLoad);
            Controls.Add(groupBox1);
            Controls.Add(dgvCustomers);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng";
            Load += FormCustomerManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.TextBox txtRewardPoints;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.TextBox txtMembershipRank;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearchKeyword;
        private System.Windows.Forms.Button btnSearch;
    }
}