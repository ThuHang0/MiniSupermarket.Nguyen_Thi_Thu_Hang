
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer? components = null;

        private Panel panelTop = null!;
        private Panel panelRight = null!;

        private Label lblHeading = null!;

        private DataGridView dgvProducts = null!;

        private TextBox txtId = null!;
        private TextBox txtBarcode = null!;
        private TextBox txtProductName = null!;
        private TextBox txtSearchBarcode = null!;

        private NumericUpDown nudPrice = null!;
        private NumericUpDown nudStock = null!;

        private ComboBox cboCategory = null!;
        private ComboBox cboFilterCategory = null!;

        private Button btnSearch = null!;
        private Button btnLoad = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblHeading = new Label();
            txtSearchBarcode = new TextBox();
            cboFilterCategory = new ComboBox();
            btnSearch = new Button();
            panelRight = new Panel();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dgvProducts = new DataGridView();
            txtId = new TextBox();
            txtBarcode = new TextBox();
            txtProductName = new TextBox();
            nudPrice = new NumericUpDown();
            nudStock = new NumericUpDown();
            cboCategory = new ComboBox();
            panelTop.SuspendLayout();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblHeading);
            panelTop.Controls.Add(txtSearchBarcode);
            panelTop.Controls.Add(cboFilterCategory);
            panelTop.Controls.Add(btnSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1000, 95);
            panelTop.TabIndex = 2;
            // 
            // lblHeading
            // 
            lblHeading.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(30, 58, 95);
            lblHeading.Location = new Point(20, 8);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(600, 35);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "QUẢN LÝ SẢN PHẨM & KHO HÀNG";
            // 
            // txtSearchBarcode
            // 
            txtSearchBarcode.Location = new Point(20, 52);
            txtSearchBarcode.Name = "txtSearchBarcode";
            txtSearchBarcode.PlaceholderText = "Tìm theo mã vạch hoặc tên...";
            txtSearchBarcode.Size = new Size(240, 25);
            txtSearchBarcode.TabIndex = 1;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Location = new Point(275, 52);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(180, 25);
            cboFilterCategory.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.SteelBlue;
            btnSearch.Location = new Point(0, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 3;
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // panelRight
            // 
            panelRight.AutoScroll = true;
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(btnLoad);
            panelRight.Controls.Add(btnAdd);
            panelRight.Controls.Add(btnUpdate);
            panelRight.Controls.Add(btnDelete);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(680, 95);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(12);
            panelRight.Size = new Size(320, 555);
            panelRight.TabIndex = 1;
            panelRight.Paint += panelRight_Paint;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.Gray;
            btnLoad.Location = new Point(0, 0);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 0;
            btnLoad.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SeaGreen;
            btnAdd.Location = new Point(0, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 1;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.RoyalBlue;
            btnUpdate.Location = new Point(0, 0);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 2;
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Firebrick;
            btnDelete.Location = new Point(0, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 3;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(0, 95);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(680, 555);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // txtId
            // 
            txtId.Location = new Point(0, 0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(100, 23);
            txtId.TabIndex = 0;
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(0, 0);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(100, 23);
            txtBarcode.TabIndex = 0;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(0, 0);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(100, 23);
            txtProductName.TabIndex = 0;
            // 
            // nudPrice
            // 
            nudPrice.Location = new Point(0, 0);
            nudPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(120, 23);
            nudPrice.TabIndex = 0;
            nudPrice.ThousandsSeparator = true;
            // 
            // nudStock
            // 
            nudStock.Location = new Point(0, 0);
            nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(120, 23);
            nudStock.TabIndex = 0;
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(0, 0);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(121, 23);
            cboCategory.TabIndex = 0;
            // 
            // FormProductManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1000, 650);
            Controls.Add(dgvProducts);
            Controls.Add(panelRight);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 10F);
            Name = "FormProductManagement";
            Text = "Quản lý Sản phẩm";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ResumeLayout(false);
        }

        private void AddField(
            Panel parent,
            string labelText,
            Control input,
            int y)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(15, y),
                Size = new Size(285, 25),
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold)
            };

            input.Location = new Point(15, y + 27);
            input.Size = new Size(285, 30);

            parent.Controls.Add(label);
            parent.Controls.Add(input);
        }

        private void SetupButton(
            Button btn,
            string text,
            int x,
            int y,
            int width)
        {
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(width, 40);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
        }
    }
}
