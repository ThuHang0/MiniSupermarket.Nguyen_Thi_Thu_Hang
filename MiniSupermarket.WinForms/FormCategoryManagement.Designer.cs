namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.colCategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelReady = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelUrl = new System.Windows.Forms.ToolStripStatusLabel();

            this.gbSearch.SuspendLayout();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.btnLoad);
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtKeyword);
            this.gbSearch.Location = new System.Drawing.Point(12, 12);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(760, 60);
            this.gbSearch.TabIndex = 0;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Tìm kiếm";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(420, 22);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 25);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Tải lại";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(330, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(15, 24);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PlaceholderText = "Nhập từ khóa...";
            this.txtKeyword.Size = new System.Drawing.Size(300, 23);
            this.txtKeyword.TabIndex = 0;
            // 
            // gbList
            // 
            this.gbList.Controls.Add(this.dgvCategories);
            this.gbList.Location = new System.Drawing.Point(12, 78);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(460, 340);
            this.gbList.TabIndex = 1;
            this.gbList.TabStop = false;
            this.gbList.Text = "Danh sách Nhóm hàng";
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCategoryId,
            this.colCategoryName,
            this.colDescription});
            this.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategories.Location = new System.Drawing.Point(3, 19);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersWidth = 51;
            this.dgvCategories.RowTemplate.Height = 25;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(454, 318);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_CellClick);
            // 
            // colCategoryId
            // 
            this.colCategoryId.DataPropertyName = "CategoryId";
            this.colCategoryId.HeaderText = "Mã ID";
            this.colCategoryId.Name = "colCategoryId";
            this.colCategoryId.ReadOnly = true;
            // 
            // colCategoryName
            // 
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "Tên Nhóm hàng";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Mô Tả";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.btnDelete);
            this.gbDetails.Controls.Add(this.btnUpdate);
            this.gbDetails.Controls.Add(this.btnAdd);
            this.gbDetails.Controls.Add(this.txtDescription);
            this.gbDetails.Controls.Add(this.lblDescription);
            this.gbDetails.Controls.Add(this.txtCategoryName);
            this.gbDetails.Controls.Add(this.lblCategoryName);
            this.gbDetails.Controls.Add(this.txtId);
            this.gbDetails.Controls.Add(this.lblId);
            this.gbDetails.Location = new System.Drawing.Point(478, 78);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(294, 340);
            this.gbDetails.TabIndex = 2;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Thông tin Nhóm hàng";
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
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(15, 114);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(264, 23);
            this.txtCategoryName.TabIndex = 3;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(15, 96);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(206, 15);
            this.lblCategoryName.TabIndex = 2;
            this.lblCategoryName.Text = "Tên Nhóm hàng (Ví dụ: Bánh kẹo)";
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelReady,
            this.toolStripStatusLabelUrl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelReady
            // 
            this.toolStripStatusLabelReady.Name = "toolStripStatusLabelReady";
            this.toolStripStatusLabelReady.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabelReady.Text = "Ready ";
            // 
            // toolStripStatusLabelUrl
            // 
            this.toolStripStatusLabelUrl.Name = "toolStripStatusLabelUrl";
            this.toolStripStatusLabelUrl.Size = new System.Drawing.Size(217, 17);
            this.toolStripStatusLabelUrl.Text = "https://localhost:7123/api/categories";
            // 
            // FormCategoryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.gbSearch);
            this.Name = "FormCategoryManagement";
            this.Text = "Quản lý Danh mục Nhóm hàng - FormCategoryManagement";
            this.Load += new System.EventHandler(this.FormCategoryManagement_Load);

            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelReady;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUrl;
    }
}