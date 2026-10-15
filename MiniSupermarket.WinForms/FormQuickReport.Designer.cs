namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer? components = null;

        private DateTimePicker dtpReportDate = null!;
        private Button btnRunReport = null!;

        private Label lblTotalOrders = null!;
        private Label lblTotalRevenue = null!;
        private Label lblBestSeller = null!;
        private Label lblReportStatus = null!;

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
            dtpReportDate = new DateTimePicker();
            btnRunReport = new Button();

            lblTotalOrders = new Label();
            lblTotalRevenue = new Label();
            lblBestSeller = new Label();
            lblReportStatus = new Label();

            SuspendLayout();

            // THANH CHỌN NGÀY
            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(15),
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White
            };

            var lblDate = new Label
            {
                Text = "Ngày báo cáo:",
                AutoSize = true,
                Margin = new Padding(3, 9, 10, 0)
            };

            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Format = DateTimePickerFormat.Custom;
            dtpReportDate.CustomFormat = "dd/MM/yyyy";
            dtpReportDate.Width = 160;
            dtpReportDate.Margin = new Padding(3, 3, 15, 3);

            btnRunReport.Name = "btnRunReport";
            btnRunReport.Text = "Xem báo cáo";
            btnRunReport.Size = new Size(150, 36);
            btnRunReport.BackColor = Color.SteelBlue;
            btnRunReport.ForeColor = Color.White;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.Click += btnRunReport_Click;

            toolbar.Controls.AddRange(new Control[]
            {
                lblDate,
                dtpReportDate,
                btnRunReport
            });

            // BA THẺ THỐNG KÊ
            var cards = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 280,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(8, 15, 8, 8)
            };

            cards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 30F));

            cards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 35F));

            cards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 35F));

            cards.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            cards.Controls.Add(
                CreateReportCard(
                    "TỔNG SỐ HÓA ĐƠN",
                    lblTotalOrders,
                    Color.SteelBlue),
                0,
                0);

            cards.Controls.Add(
                CreateReportCard(
                    "TỔNG DOANH THU",
                    lblTotalRevenue,
                    Color.SeaGreen),
                1,
                0);

            cards.Controls.Add(
                CreateReportCard(
                    "SẢN PHẨM BÁN CHẠY",
                    lblBestSeller,
                    Color.DarkOrange),
                2,
                0);

            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblBestSeller.Name = "lblBestSeller";

            // TRẠNG THÁI BÁO CÁO
            lblReportStatus.Name = "lblReportStatus";
            lblReportStatus.Dock = DockStyle.Top;
            lblReportStatus.Height = 70;
            lblReportStatus.Padding = new Padding(20, 10, 20, 0);
            lblReportStatus.ForeColor = Color.DimGray;
            lblReportStatus.Text =
                "Chọn ngày và bấm Xem báo cáo.";

            Controls.Add(lblReportStatus);
            Controls.Add(cards);
            Controls.Add(toolbar);

            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu";
            ClientSize = new Size(1020, 600);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(244, 245, 247);
            AutoScaleMode = AutoScaleMode.Font;

            Load += FormQuickReport_Load;

            ResumeLayout(false);
        }

        private Panel CreateReportCard(
            string title,
            Label valueLabel,
            Color background)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(8),
                Padding = new Padding(12),
                BackColor = background
            };

            var heading = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font(
                    "Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            valueLabel.Dock = DockStyle.Fill;
            valueLabel.Text = "—";
            valueLabel.ForeColor = Color.White;
            valueLabel.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            valueLabel.TextAlign = ContentAlignment.MiddleCenter;

            panel.Controls.Add(valueLabel);
            panel.Controls.Add(heading);

            return panel;
        }
    }
}