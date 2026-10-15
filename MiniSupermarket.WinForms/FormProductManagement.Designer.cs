namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer? components = null;

        private TextBox txtSearchBarcode = null!;
        private ComboBox cboFilterCategory = null!;
        private Button btnSearch = null!;

        private DataGridView dgvProducts = null!;

        private TextBox txtId = null!;
        private TextBox txtBarcode = null!;
        private TextBox txtProductName = null!;
        private NumericUpDown nudPrice = null!;
        private NumericUpDown nudStock = null!;
        private ComboBox cboCategory = null!;

        private Button btnLoad = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnClear = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtSearchBarcode = new TextBox();
            cboFilterCategory = new ComboBox();
            btnSearch = new Button();

            dgvProducts = new DataGridView();

            txtId = new TextBox();
            txtBarcode = new TextBox();
            txtProductName = new TextBox();
            nudPrice = new NumericUpDown();
            nudStock = new NumericUpDown();
            cboCategory = new ComboBox();

            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            SuspendLayout();

            // THANH TÌM KIẾM
            var searchPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(8),
                AutoScroll = true,
                WrapContents = true
            };

            txtSearchBarcode.Name = "txtSearchBarcode";
            txtSearchBarcode.Width = 230;
            txtSearchBarcode.PlaceholderText =
                "Nhập mã vạch hoặc tên sản phẩm";
            txtSearchBarcode.KeyDown += txtSearchBarcode_KeyDown;

            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Width = 210;
            cboFilterCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;

            ConfigureButton(btnSearch, "Tìm kiếm", Color.SteelBlue);
            ConfigureButton(btnLoad, "Tải lại", Color.DimGray);

            btnSearch.Click += btnSearch_Click;
            btnLoad.Click += btnLoad_Click;

            searchPanel.Controls.AddRange(new Control[]
            {
                txtSearchBarcode,
                cboFilterCategory,
                btnSearch,
                btnLoad
            });

            // BỐ CỤC CHÍNH
            var body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };

            body.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));
            body.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 300F));
            body.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            // BẢNG SẢN PHẨM
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.MultiSelect = false;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            AddGridColumn("ProductId", "Mã", 45);
            AddGridColumn("Barcode", "Mã vạch", 120);
            AddGridColumn("ProductName", "Tên sản phẩm", 180);
            AddGridColumn("Price", "Giá", 90);
            AddGridColumn("StockQuantity", "Tồn", 65);
            AddGridColumn("CategoryName", "Danh mục", 120);

            dgvProducts.Columns["Price"]!
                .DefaultCellStyle.Format = "N2";

            dgvProducts.CellClick += dgvProducts_CellClick;

            // VÙNG NHẬP LIỆU
            var details = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 14,
                Padding = new Padding(12),
                AutoScroll = true,
                BackColor = Color.White
            };

            details.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < 12; i++)
            {
                details.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, 30F));
            }

            details.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 100F));
            details.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            txtId.Name = "txtId";
            txtId.ReadOnly = true;

            txtBarcode.Name = "txtBarcode";
            txtBarcode.MaxLength = 50;

            txtProductName.Name = "txtProductName";
            txtProductName.MaxLength = 150;

            nudPrice.Name = "nudPrice";
            nudPrice.Minimum = 0;
            nudPrice.Maximum = 9999999999999999.99m;
            nudPrice.DecimalPlaces = 2;
            nudPrice.ThousandsSeparator = true;

            nudStock.Name = "nudStock";
            nudStock.Minimum = 0;
            nudStock.Maximum = int.MaxValue;
            nudStock.ThousandsSeparator = true;

            cboCategory.Name = "cboCategory";
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            AddInput(details, 0, "Mã sản phẩm", txtId);
            AddInput(details, 2, "Mã vạch", txtBarcode);
            AddInput(details, 4, "Tên sản phẩm", txtProductName);
            AddInput(details, 6, "Giá bán", nudPrice);
            AddInput(details, 8, "Số lượng tồn", nudStock);
            AddInput(details, 10, "Danh mục", cboCategory);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true
            };

            ConfigureButton(btnAdd, "Thêm", Color.SeaGreen);
            ConfigureButton(btnUpdate, "Cập nhật", Color.SteelBlue);
            ConfigureButton(btnDelete, "Xóa", Color.Firebrick);
            ConfigureButton(btnClear, "Nhập mới", Color.DimGray);

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;

            actions.Controls.AddRange(new Control[]
            {
                btnAdd, btnUpdate, btnDelete, btnClear
            });

            details.Controls.Add(actions, 0, 12);

            body.Controls.Add(dgvProducts, 0, 0);
            body.Controls.Add(details, 1, 0);

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1020, 600);
            BackColor = Color.FromArgb(244, 245, 247);
            Text = "Quản lý sản phẩm";

            Controls.Add(body);
            Controls.Add(searchPanel);

            Load += FormProductManagement_Load;

            ResumeLayout(false);
        }

        private void ConfigureButton(
            Button button,
            string text,
            Color color)
        {
            button.Text = text;
            button.Size = new Size(115, 36);
            button.Margin = new Padding(3);
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        private void AddGridColumn(
            string property,
            string title,
            int weight)
        {
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = property,
                DataPropertyName = property,
                HeaderText = title,
                FillWeight = weight
            });
        }

        private void AddInput(
            TableLayoutPanel panel,
            int row,
            string title,
            Control input)
        {
            panel.Controls.Add(new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, row);

            input.Dock = DockStyle.Fill;
            panel.Controls.Add(input, 0, row + 1);
        }
    }
}