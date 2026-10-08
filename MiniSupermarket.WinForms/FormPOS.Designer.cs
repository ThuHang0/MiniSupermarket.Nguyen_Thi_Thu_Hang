#nullable enable

using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        private System.ComponentModel.IContainer? components = null;

        private TableLayoutPanel layoutMain = null!;
        private TableLayoutPanel layoutCart = null!;
        private TableLayoutPanel layoutPayment = null!;
        private TableLayoutPanel layoutBarcode = null!;
        private TableLayoutPanel layoutPhone = null!;
        private FlowLayoutPanel layoutCartButtons = null!;

        private Label lblCartTitle = null!;
        private Label lblBarcodeTitle = null!;
        private Label lblPaymentTitle = null!;
        private Label lblPhoneTitle = null!;
        private Label lblCustomerName = null!;
        private Label lblTotalTitle = null!;
        private Label lblTotalAmount = null!;
        private Label lblCashTitle = null!;
        private Label lblChangeTitle = null!;
        private Label lblChange = null!;
        private Label lblGuide = null!;

        private TextBox txtBarcode = null!;
        private TextBox txtCustomerPhone = null!;
        private TextBox txtCashReceived = null!;

        private DataGridView dgvCart = null!;

        private Button btnAddBarcode = null!;
        private Button btnFindCustomer = null!;
        private Button btnCheckout = null!;
        private Button btnClearCart = null!;
        private Button btnRemoveItem = null!;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            layoutMain = new TableLayoutPanel();
            layoutCart = new TableLayoutPanel();
            lblCartTitle = new Label();
            lblBarcodeTitle = new Label();
            layoutBarcode = new TableLayoutPanel();
            txtBarcode = new TextBox();
            btnAddBarcode = new Button();
            dgvCart = new DataGridView();
            layoutCartButtons = new FlowLayoutPanel();
            btnRemoveItem = new Button();
            btnClearCart = new Button();
            layoutPayment = new TableLayoutPanel();
            lblPaymentTitle = new Label();
            lblPhoneTitle = new Label();
            layoutPhone = new TableLayoutPanel();
            txtCustomerPhone = new TextBox();
            btnFindCustomer = new Button();
            lblCustomerName = new Label();
            lblTotalTitle = new Label();
            lblTotalAmount = new Label();
            lblCashTitle = new Label();
            txtCashReceived = new TextBox();
            lblChangeTitle = new Label();
            lblChange = new Label();
            btnCheckout = new Button();
            lblGuide = new Label();
            layoutMain.SuspendLayout();
            layoutCart.SuspendLayout();
            layoutBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            layoutCartButtons.SuspendLayout();
            layoutPayment.SuspendLayout();
            layoutPhone.SuspendLayout();
            SuspendLayout();
            // 
            // layoutMain
            // 
            layoutMain.BackColor = Color.FromArgb(244, 245, 247);
            layoutMain.ColumnCount = 2;
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            layoutMain.Controls.Add(layoutCart, 0, 0);
            layoutMain.Controls.Add(layoutPayment, 1, 0);
            layoutMain.Dock = DockStyle.Fill;
            layoutMain.Location = new Point(0, 0);
            layoutMain.Name = "layoutMain";
            layoutMain.Padding = new Padding(12);
            layoutMain.RowCount = 1;
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMain.Size = new Size(1000, 650);
            layoutMain.TabIndex = 0;
            // 
            // layoutCart
            // 
            layoutCart.BackColor = Color.White;
            layoutCart.ColumnCount = 1;
            layoutCart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCart.Controls.Add(lblCartTitle, 0, 0);
            layoutCart.Controls.Add(lblBarcodeTitle, 0, 1);
            layoutCart.Controls.Add(layoutBarcode, 0, 2);
            layoutCart.Controls.Add(dgvCart, 0, 3);
            layoutCart.Controls.Add(layoutCartButtons, 0, 4);
            layoutCart.Dock = DockStyle.Fill;
            layoutCart.Location = new Point(12, 12);
            layoutCart.Margin = new Padding(0, 0, 12, 0);
            layoutCart.Name = "layoutCart";
            layoutCart.Padding = new Padding(12);
            layoutCart.RowCount = 5;
            layoutCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutCart.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCart.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layoutCart.Size = new Size(622, 626);
            layoutCart.TabIndex = 0;
            // 
            // lblCartTitle
            // 
            lblCartTitle.Dock = DockStyle.Fill;
            lblCartTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblCartTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblCartTitle.Location = new Point(15, 12);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(592, 42);
            lblCartTitle.TabIndex = 0;
            lblCartTitle.Text = "GIỎ HÀNG";
            // 
            // lblBarcodeTitle
            // 
            lblBarcodeTitle.Dock = DockStyle.Fill;
            lblBarcodeTitle.Location = new Point(15, 54);
            lblBarcodeTitle.Name = "lblBarcodeTitle";
            lblBarcodeTitle.Size = new Size(592, 28);
            lblBarcodeTitle.TabIndex = 1;
            lblBarcodeTitle.Text = "Nhập hoặc quét mã vạch:";
            lblBarcodeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutBarcode
            // 
            layoutBarcode.ColumnCount = 2;
            layoutBarcode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBarcode.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutBarcode.Controls.Add(txtBarcode, 0, 0);
            layoutBarcode.Controls.Add(btnAddBarcode, 1, 0);
            layoutBarcode.Dock = DockStyle.Fill;
            layoutBarcode.Location = new Point(12, 82);
            layoutBarcode.Margin = new Padding(0);
            layoutBarcode.Name = "layoutBarcode";
            layoutBarcode.RowCount = 1;
            layoutBarcode.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutBarcode.Size = new Size(598, 48);
            layoutBarcode.TabIndex = 2;
            // 
            // txtBarcode
            // 
            txtBarcode.Dock = DockStyle.Fill;
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(0, 4);
            txtBarcode.Margin = new Padding(0, 4, 8, 4);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Mã vạch → Enter";
            txtBarcode.Size = new Size(490, 29);
            txtBarcode.TabIndex = 0;
            // 
            // btnAddBarcode
            // 
            btnAddBarcode.BackColor = Color.FromArgb(37, 99, 235);
            btnAddBarcode.Dock = DockStyle.Fill;
            btnAddBarcode.FlatAppearance.BorderSize = 0;
            btnAddBarcode.FlatStyle = FlatStyle.Flat;
            btnAddBarcode.ForeColor = Color.White;
            btnAddBarcode.Location = new Point(498, 2);
            btnAddBarcode.Margin = new Padding(0, 2, 0, 6);
            btnAddBarcode.Name = "btnAddBarcode";
            btnAddBarcode.Size = new Size(100, 40);
            btnAddBarcode.TabIndex = 1;
            btnAddBarcode.Text = "Thêm";
            btnAddBarcode.UseVisualStyleBackColor = false;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dgvCart.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCart.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCart.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvCart.DefaultCellStyle = dataGridViewCellStyle3;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.Location = new Point(15, 133);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersVisible = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(592, 423);
            dgvCart.TabIndex = 3;
            // 
            // layoutCartButtons
            // 
            layoutCartButtons.Controls.Add(btnRemoveItem);
            layoutCartButtons.Controls.Add(btnClearCart);
            layoutCartButtons.Dock = DockStyle.Fill;
            layoutCartButtons.Location = new Point(15, 562);
            layoutCartButtons.Name = "layoutCartButtons";
            layoutCartButtons.Padding = new Padding(0, 8, 0, 0);
            layoutCartButtons.Size = new Size(592, 49);
            layoutCartButtons.TabIndex = 4;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Location = new Point(3, 11);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(140, 38);
            btnRemoveItem.TabIndex = 0;
            btnRemoveItem.Text = "Xóa dòng chọn";
            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(220, 38, 38);
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(149, 11);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(140, 38);
            btnClearCart.TabIndex = 1;
            btnClearCart.Text = "Hủy giỏ hàng";
            btnClearCart.UseVisualStyleBackColor = false;
            // 
            // layoutPayment
            // 
            layoutPayment.AutoScroll = true;
            layoutPayment.BackColor = Color.White;
            layoutPayment.ColumnCount = 1;
            layoutPayment.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPayment.Controls.Add(lblPaymentTitle, 0, 0);
            layoutPayment.Controls.Add(lblPhoneTitle, 0, 1);
            layoutPayment.Controls.Add(layoutPhone, 0, 2);
            layoutPayment.Controls.Add(lblCustomerName, 0, 3);
            layoutPayment.Controls.Add(lblTotalTitle, 0, 4);
            layoutPayment.Controls.Add(lblTotalAmount, 0, 5);
            layoutPayment.Controls.Add(lblCashTitle, 0, 6);
            layoutPayment.Controls.Add(txtCashReceived, 0, 7);
            layoutPayment.Controls.Add(lblChangeTitle, 0, 8);
            layoutPayment.Controls.Add(lblChange, 0, 9);
            layoutPayment.Controls.Add(btnCheckout, 0, 10);
            layoutPayment.Controls.Add(lblGuide, 0, 11);
            layoutPayment.Dock = DockStyle.Fill;
            layoutPayment.Location = new Point(646, 12);
            layoutPayment.Margin = new Padding(0);
            layoutPayment.Name = "layoutPayment";
            layoutPayment.Padding = new Padding(16);
            layoutPayment.RowCount = 12;
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layoutPayment.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layoutPayment.Size = new Size(342, 626);
            layoutPayment.TabIndex = 1;
            // 
            // lblPaymentTitle
            // 
            lblPaymentTitle.Dock = DockStyle.Fill;
            lblPaymentTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPaymentTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPaymentTitle.Location = new Point(19, 16);
            lblPaymentTitle.Name = "lblPaymentTitle";
            lblPaymentTitle.Size = new Size(304, 42);
            lblPaymentTitle.TabIndex = 0;
            lblPaymentTitle.Text = "THANH TOÁN";
            // 
            // lblPhoneTitle
            // 
            lblPhoneTitle.Dock = DockStyle.Fill;
            lblPhoneTitle.Location = new Point(19, 58);
            lblPhoneTitle.Name = "lblPhoneTitle";
            lblPhoneTitle.Size = new Size(304, 28);
            lblPhoneTitle.TabIndex = 1;
            lblPhoneTitle.Text = "Số điện thoại khách hàng:";
            lblPhoneTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutPhone
            // 
            layoutPhone.ColumnCount = 2;
            layoutPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            layoutPhone.Controls.Add(txtCustomerPhone, 0, 0);
            layoutPhone.Controls.Add(btnFindCustomer, 1, 0);
            layoutPhone.Dock = DockStyle.Fill;
            layoutPhone.Location = new Point(16, 86);
            layoutPhone.Margin = new Padding(0);
            layoutPhone.Name = "layoutPhone";
            layoutPhone.RowCount = 1;
            layoutPhone.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutPhone.Size = new Size(310, 45);
            layoutPhone.TabIndex = 2;
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Dock = DockStyle.Fill;
            txtCustomerPhone.Location = new Point(0, 4);
            txtCustomerPhone.Margin = new Padding(0, 4, 6, 4);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Bỏ trống nếu khách lẻ";
            txtCustomerPhone.Size = new Size(219, 25);
            txtCustomerPhone.TabIndex = 0;
            // 
            // btnFindCustomer
            // 
            btnFindCustomer.Dock = DockStyle.Fill;
            btnFindCustomer.Location = new Point(225, 2);
            btnFindCustomer.Margin = new Padding(0, 2, 0, 6);
            btnFindCustomer.Name = "btnFindCustomer";
            btnFindCustomer.Size = new Size(85, 37);
            btnFindCustomer.TabIndex = 1;
            btnFindCustomer.Text = "Tra cứu";
            // 
            // lblCustomerName
            // 
            lblCustomerName.Dock = DockStyle.Fill;
            lblCustomerName.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustomerName.Location = new Point(19, 131);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(304, 60);
            lblCustomerName.TabIndex = 3;
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.Dock = DockStyle.Fill;
            lblTotalTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalTitle.Location = new Point(19, 191);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(304, 30);
            lblTotalTitle.TabIndex = 4;
            lblTotalTitle.Text = "TỔNG TIỀN";
            lblTotalTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Dock = DockStyle.Fill;
            lblTotalAmount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(22, 163, 74);
            lblTotalAmount.Location = new Point(19, 221);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(304, 65);
            lblTotalAmount.TabIndex = 5;
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCashTitle
            // 
            lblCashTitle.Dock = DockStyle.Fill;
            lblCashTitle.Location = new Point(19, 286);
            lblCashTitle.Name = "lblCashTitle";
            lblCashTitle.Size = new Size(304, 30);
            lblCashTitle.TabIndex = 6;
            lblCashTitle.Text = "Tiền khách đưa:";
            lblCashTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCashReceived
            // 
            txtCashReceived.Dock = DockStyle.Fill;
            txtCashReceived.Font = new Font("Segoe UI", 14F);
            txtCashReceived.Location = new Point(16, 319);
            txtCashReceived.Margin = new Padding(0, 3, 0, 3);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.PlaceholderText = "Ví dụ: 100000";
            txtCashReceived.Size = new Size(310, 32);
            txtCashReceived.TabIndex = 7;
            // 
            // lblChangeTitle
            // 
            lblChangeTitle.Dock = DockStyle.Fill;
            lblChangeTitle.Location = new Point(19, 361);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(304, 30);
            lblChangeTitle.TabIndex = 8;
            lblChangeTitle.Text = "Tiền thừa trả khách:";
            lblChangeTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblChange
            // 
            lblChange.Dock = DockStyle.Fill;
            lblChange.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblChange.Location = new Point(19, 391);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(304, 50);
            lblChange.TabIndex = 9;
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(22, 163, 74);
            btnCheckout.Dock = DockStyle.Fill;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(16, 446);
            btnCheckout.Margin = new Padding(0, 5, 0, 5);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(310, 50);
            btnCheckout.TabIndex = 10;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            // 
            // lblGuide
            // 
            lblGuide.Dock = DockStyle.Fill;
            lblGuide.ForeColor = Color.FromArgb(100, 116, 139);
            lblGuide.Location = new Point(19, 501);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(304, 109);
            lblGuide.TabIndex = 11;
            lblGuide.Text = "Enter: thêm mã vạch hoặc tra khách.\nF9: thanh toán.";
            lblGuide.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(layoutMain);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            Name = "FormPOS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng POS";
            layoutMain.ResumeLayout(false);
            layoutCart.ResumeLayout(false);
            layoutBarcode.ResumeLayout(false);
            layoutBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            layoutCartButtons.ResumeLayout(false);
            layoutPayment.ResumeLayout(false);
            layoutPayment.PerformLayout();
            layoutPhone.ResumeLayout(false);
            layoutPhone.PerformLayout();
            ResumeLayout(false);
        }
    }
}