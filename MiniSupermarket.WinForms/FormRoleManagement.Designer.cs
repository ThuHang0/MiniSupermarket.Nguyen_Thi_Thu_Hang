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
            this.gbList = new System.Windows.Forms.GroupBox();
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbList
            // 
            this.gbList.Controls.Add(this.dgvRoles);
            this.gbList.Location = new System.Drawing.Point(12, 12);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(460, 400);
            this.gbList.TabIndex = 0;
            this.gbList.TabStop = false;
            this.gbList.Text = "Danh sách Vai trò";
            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AllowUserToDeleteRows = false;
            this.dgvRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colRoleName,
            this.colDescription});
            this.dgvRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoles.Location = new System.Drawing.Point(3, 19);
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.ReadOnly = true;
            this.dgvRoles.RowHeadersWidth = 51;
            this.dgvRoles.RowTemplate.Height = 25;
            this.dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoles.Size = new System.Drawing.Size(454, 378);
            this.dgvRoles.TabIndex = 0;
            this.dgvRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoles_CellClick);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "Mã ID";
            this.colId.Name = "Id";
            this.colId.ReadOnly = true;
            // 
            // colRoleName
            // 
            this.colRoleName.DataPropertyName = "RoleName";
            this.colRoleName.HeaderText = "Tên Vai trò";
            this.colRoleName.Name = "RoleName";
            this.colRoleName.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Mô Tả";
            this.colDescription.Name = "Description";
            this.colDescription.ReadOnly = true;
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.btnDelete);
            this.gbDetails.Controls.Add(this.btnUpdate);
            this.gbDetails.Controls.Add(this.btnAdd);
            this.gbDetails.Controls.Add(this.txtDescription);
            this.gbDetails.Controls.Add(this.lblDescription);
            this.gbDetails.Controls.Add(this.txtRoleName);
            this.gbDetails.Controls.Add(this.lblRoleName);
            this.gbDetails.Controls.Add(this.txtId);
            this.gbDetails.Controls.Add(this.lblId);
            this.gbDetails.Location = new System.Drawing.Point(478, 12);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(294, 400);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Thông tin Vai trò";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(204, 273);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(109, 273);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(15, 273);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(15, 172);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(264, 80);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(15, 154);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(183, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Mô Tả (Mô tả chi tiết...)";
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(15, 114);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(264, 23);
            this.txtRoleName.TabIndex = 3;
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Location = new System.Drawing.Point(15, 96);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(193, 15);
            this.lblRoleName.TabIndex = 2;
            this.lblRoleName.Text = "Tên Vai trò (Ví dụ: Admin, User)";
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtId.Location = new System.Drawing.Point(15, 55);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(264, 23);
            this.txtId.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(15, 37);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(39, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Mã ID";
            // 
            // FormRoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 431);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbList);
            this.Name = "FormRoleManagement";
            this.Text = "Quản lý Vai trò - FormRoleManagement";
            this.Load += new System.EventHandler(this.FormRoleManagement_Load);
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.ResumeLayout(false);

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