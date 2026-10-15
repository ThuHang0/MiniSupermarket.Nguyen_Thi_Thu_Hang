namespace MiniSupermarket.WinForms
{
    partial class FormRoleManagement
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
            gbList = new GroupBox();
            dgvRoles = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colRoleName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            gbDetails = new GroupBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtRoleName = new TextBox();
            lblRoleName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            gbDetails.SuspendLayout();
            SuspendLayout();
            // 
            // gbList
            // 
            gbList.Controls.Add(dgvRoles);
            gbList.Location = new Point(12, 12);
            gbList.Name = "gbList";
            gbList.Size = new Size(460, 400);
            gbList.TabIndex = 0;
            gbList.TabStop = false;
            gbList.Text = "Danh sách Vai trò";
            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Columns.AddRange(new DataGridViewColumn[] { colId, colRoleName, colDescription });
            dgvRoles.Dock = DockStyle.Fill;
            dgvRoles.Location = new Point(3, 19);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.RowHeadersWidth = 51;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(454, 378);
            dgvRoles.TabIndex = 0;
            dgvRoles.CellClick += dgvRoles_CellClick;
            dgvRoles.CellContentClick += dgvRoles_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colRoleName
            // 
            colRoleName.DataPropertyName = "RoleName";
            colRoleName.HeaderText = "Tên Vai trò";
            colRoleName.Name = "colRoleName";
            colRoleName.ReadOnly = true;
            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.HeaderText = "Mô Tả";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // gbDetails
            // 
            gbDetails.Controls.Add(btnDelete);
            gbDetails.Controls.Add(btnUpdate);
            gbDetails.Controls.Add(btnAdd);
            gbDetails.Controls.Add(txtDescription);
            gbDetails.Controls.Add(lblDescription);
            gbDetails.Controls.Add(txtRoleName);
            gbDetails.Controls.Add(lblRoleName);
            gbDetails.Controls.Add(txtId);
            gbDetails.Controls.Add(lblId);
            gbDetails.Location = new Point(478, 12);
            gbDetails.Name = "gbDetails";
            gbDetails.Size = new Size(294, 400);
            gbDetails.TabIndex = 1;
            gbDetails.TabStop = false;
            gbDetails.Text = "Thông tin Vai trò";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(204, 273);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(109, 273);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 273);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(15, 172);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(264, 80);
            txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(15, 154);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(130, 15);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô Tả (Mô tả chi tiết...)";
            // 
            // txtRoleName
            // 
            txtRoleName.Location = new Point(15, 114);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new Size(264, 23);
            txtRoleName.TabIndex = 3;
            // 
            // lblRoleName
            // 
            lblRoleName.AutoSize = true;
            lblRoleName.Location = new Point(15, 96);
            lblRoleName.Name = "lblRoleName";
            lblRoleName.Size = new Size(170, 15);
            lblRoleName.TabIndex = 2;
            lblRoleName.Text = "Tên Vai trò (Ví dụ: Admin, User)";
            // 
            // txtId
            // 
            txtId.BackColor = SystemColors.ControlLight;
            txtId.Location = new Point(15, 55);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(264, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(15, 37);
            lblId.Name = "lblId";
            lblId.Size = new Size(38, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID";
            // 
            // FormRoleManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 431);
            Controls.Add(gbDetails);
            Controls.Add(gbList);
            Name = "FormRoleManagement";
            Text = "Quản lý Vai trò - FormRoleManagement";
            Load += FormRoleManagement_Load;
            gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            gbDetails.ResumeLayout(false);
            gbDetails.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
    }
}