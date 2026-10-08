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
            gbSearch = new GroupBox();
            btnLoad = new Button();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            gbList = new GroupBox();
            dgvCategories = new DataGridView();
            colCategoryId = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            gbDetails = new GroupBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelReady = new ToolStripStatusLabel();
            toolStripStatusLabelUrl = new ToolStripStatusLabel();
            gbSearch.SuspendLayout();
            gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            gbDetails.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gbSearch
            // 
            gbSearch.Controls.Add(btnLoad);
            gbSearch.Controls.Add(btnSearch);
            gbSearch.Controls.Add(txtKeyword);
            gbSearch.Location = new Point(12, 12);
            gbSearch.Name = "gbSearch";
            gbSearch.Size = new Size(760, 60);
            gbSearch.TabIndex = 0;
            gbSearch.TabStop = false;
            gbSearch.Text = "Tìm kiếm";
            gbSearch.Enter += gbSearch_Enter;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(420, 22);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(80, 25);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(330, 22);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 25);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(15, 24);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập từ khóa...";
            txtKeyword.Size = new Size(300, 23);
            txtKeyword.TabIndex = 0;
            // 
            // gbList
            // 
            gbList.Controls.Add(dgvCategories);
            gbList.Location = new Point(12, 78);
            gbList.Name = "gbList";
            gbList.Size = new Size(460, 340);
            gbList.TabIndex = 1;
            gbList.TabStop = false;
            gbList.Text = "Danh sách Nhóm hàng";
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colCategoryId, colCategoryName, colDescription });
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(3, 19);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(454, 318);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            // 
            // colCategoryId
            // 
            colCategoryId.DataPropertyName = "CategoryId";
            colCategoryId.HeaderText = "Mã ID";
            colCategoryId.Name = "colCategoryId";
            colCategoryId.ReadOnly = true;
            // 
            // colCategoryName
            // 
            colCategoryName.DataPropertyName = "CategoryName";
            colCategoryName.HeaderText = "Tên Nhóm hàng";
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
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
            gbDetails.Controls.Add(txtCategoryName);
            gbDetails.Controls.Add(lblCategoryName);
            gbDetails.Controls.Add(txtId);
            gbDetails.Controls.Add(lblId);
            gbDetails.Location = new Point(478, 78);
            gbDetails.Name = "gbDetails";
            gbDetails.Size = new Size(294, 340);
            gbDetails.TabIndex = 2;
            gbDetails.TabStop = false;
            gbDetails.Text = "Thông tin Nhóm hàng";
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
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(15, 114);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(264, 23);
            txtCategoryName.TabIndex = 3;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(15, 96);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(185, 15);
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên Nhóm hàng (Ví dụ: Bánh kẹo)";
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
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelReady, toolStripStatusLabelUrl });
            statusStrip1.Location = new Point(0, 439);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(784, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelReady
            // 
            toolStripStatusLabelReady.Name = "toolStripStatusLabelReady";
            toolStripStatusLabelReady.Size = new Size(42, 17);
            toolStripStatusLabelReady.Text = "Ready ";
            // 
            // toolStripStatusLabelUrl
            // 
            toolStripStatusLabelUrl.Name = "toolStripStatusLabelUrl";
            toolStripStatusLabelUrl.Size = new Size(202, 17);
            toolStripStatusLabelUrl.Text = "https://localhost:7123/api/categories";
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(statusStrip1);
            Controls.Add(gbDetails);
            Controls.Add(gbList);
            Controls.Add(gbSearch);
            Name = "FormCategoryManagement";
            Text = "Quản lý Danh mục Nhóm hàng - FormCategoryManagement";
            Load += FormCategoryManagement_Load;
            gbSearch.ResumeLayout(false);
            gbSearch.PerformLayout();
            gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            gbDetails.ResumeLayout(false);
            gbDetails.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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