using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" },
                    { 2, "Nước giải khát", "Nước ngọt, nước suối, trà đóng chai" },
                    { 3, "Sữa & Đồ uống dinh dưỡng", "Sữa tươi, sữa hộp, sữa dinh dưỡng" },
                    { 4, "Mì & Thực phẩm ăn liền", "Mì gói, cháo ăn liền, phở ăn liền" },
                    { 5, "Gia vị nấu ăn", "Nước mắm, nước tương, muối, đường" },
                    { 6, "Dầu ăn", "Dầu đậu nành, dầu hướng dương, dầu thực vật" },
                    { 7, "Gạo & Thực phẩm khô", "Gạo, đậu, bột và các loại thực phẩm khô" },
                    { 8, "Đồ hộp", "Cá hộp, thịt hộp, pate và thực phẩm đóng hộp" },
                    { 9, "Cà phê & Trà", "Cà phê hòa tan, cà phê rang xay và trà" },
                    { 10, "Bánh mì & Bánh ngọt", "Bánh mì, bánh bông lan và bánh ngọt" },
                    { 11, "Kem & Thực phẩm đông lạnh", "Kem, xúc xích và các loại thực phẩm đông lạnh" },
                    { 12, "Đồ dùng nhà bếp", "Màng bọc thực phẩm, túi đựng và đồ dùng nhà bếp" },
                    { 13, "Giấy & Khăn giấy", "Khăn giấy, giấy vệ sinh và giấy lau" },
                    { 14, "Chăm sóc cá nhân", "Dầu gội, sữa tắm và sản phẩm vệ sinh cá nhân" },
                    { 15, "Kem đánh răng & Bàn chải", "Kem đánh răng, bàn chải và sản phẩm chăm sóc răng miệng" },
                    { 16, "Nước giặt & Nước xả", "Nước giặt, bột giặt và nước xả vải" },
                    { 17, "Nước rửa chén & Vệ sinh", "Nước rửa chén và các sản phẩm vệ sinh nhà cửa" },
                    { 18, "Đồ dùng học tập", "Bút, vở, thước và các dụng cụ học tập" },
                    { 19, "Đồ dùng cá nhân", "Khẩu trang, bàn chải, lược và đồ dùng cá nhân" },
                    { 20, "Đồ gia dụng", "Các vật dụng gia đình và đồ dùng sinh hoạt hằng ngày" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "125 Nguyễn Trãi, Phường Bến Thành, TP. Hồ Chí Minh", "Nguyễn Văn Anh", "Vàng", "0901122334", 150 },
                    { 2, "45 Lê Lợi, Phường Sài Gòn, TP. Hồ Chí Minh", "Trần Thị Bích Ngọc", "Bạc", "0918877665", 50 },
                    { 3, "78 Nguyễn Văn Linh, Phường Tân Hưng, TP. Hồ Chí Minh", "Lê Văn Cường", "Chuẩn", "0983344556", 10 },
                    { 4, "230 Điện Biên Phủ, Phường Gia Định, TP. Hồ Chí Minh", "Phạm Thị Diễm My", "Vàng", "0934455667", 200 },
                    { 5, "56 Phan Văn Trị, Phường Gò Vấp, TP. Hồ Chí Minh", "Hoàng Văn Đức", "Bạc", "0977788990", 80 },
                    { 6, "102 Quang Trung, Phường Thông Tây Hội, TP. Hồ Chí Minh", "Võ Thị Thanh Hương", "Chuẩn", "0961234567", 25 },
                    { 7, "88 Nguyễn Xí, Phường Bình Lợi Trung, TP. Hồ Chí Minh", "Đặng Văn Giang", "Vàng", "0923456789", 175 },
                    { 8, "15 Cách Mạng Tháng Tám, Phường Bàn Cờ, TP. Hồ Chí Minh", "Bùi Thị Kim Ngân", "Bạc", "0902233445", 65 },
                    { 9, "67 Trường Chinh, Phường Tân Bình, TP. Hồ Chí Minh", "Đỗ Minh Khang", "Chuẩn", "0913344556", 30 },
                    { 10, "190 Hoàng Văn Thụ, Phường Phú Nhuận, TP. Hồ Chí Minh", "Nguyễn Thị Thu Hà", "Vàng", "0984455667", 220 },
                    { 11, "25 Lạc Long Quân, Phường Hòa Bình, TP. Hồ Chí Minh", "Trần Quốc Bảo", "Bạc", "0935566778", 90 },
                    { 12, "140 Âu Cơ, Phường Tân Hòa, TP. Hồ Chí Minh", "Lê Thị Minh Châu", "Chuẩn", "0976677889", 15 },
                    { 13, "39 Võ Văn Ngân, Phường Thủ Đức, TP. Hồ Chí Minh", "Phan Thanh Tùng", "Vàng", "0967788991", 300 },
                    { 14, "81 Kha Vạn Cân, Phường Linh Xuân, TP. Hồ Chí Minh", "Huỳnh Thị Lan Anh", "Bạc", "0928899001", 110 },
                    { 15, "210 Nguyễn Duy Trinh, Phường Long Trường, TP. Hồ Chí Minh", "Ngô Văn Phúc", "Chuẩn", "0909900112", 35 },
                    { 16, "72 Tô Ngọc Vân, Phường Tam Bình, TP. Hồ Chí Minh", "Mai Thị Ngọc Hân", "Vàng", "0911011223", 250 },
                    { 17, "48 Phạm Văn Đồng, Phường Hiệp Bình, TP. Hồ Chí Minh", "Trương Minh Quân", "Bạc", "0982122334", 75 },
                    { 18, "115 Lê Văn Việt, Phường Tăng Nhơn Phú, TP. Hồ Chí Minh", "Đinh Thị Mỹ Linh", "Chuẩn", "0933233445", 40 },
                    { 19, "93 Nguyễn Thị Thập, Phường Tân Mỹ, TP. Hồ Chí Minh", "Cao Văn Thành", "Vàng", "0974344556", 180 },
                    { 20, "160 Huỳnh Tấn Phát, Phường Tân Thuận, TP. Hồ Chí Minh", "Dương Thị Thùy Trang", "Bạc", "0965455667", 95 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8931111111111", 1, 12000m, "Snack Khoai Tây Lay's Vị Tự Nhiên", 50 },
                    { 2, "8932222222222", 2, 10000m, "Nước Ngọt Coca-Cola Lon 320ml", 120 },
                    { 3, "8933333333333", 3, 8500m, "Sữa Tươi TH True Milk 180ml", 200 },
                    { 4, "8934444444444", 4, 4500m, "Mì Hảo Hảo Tôm Chua Cay", 500 },
                    { 5, "8935555555555", 5, 25000m, "Nước Mắm Nam Ngư Đệ Nhị 900ml", 30 },
                    { 6, "8936666666666", 6, 55000m, "Dầu Ăn Simply Đậu Nành 1 Lít", 40 },
                    { 7, "8930000000007", 7, 180000m, "Gạo ST25 Túi 5kg", 35 },
                    { 8, "8930000000008", 8, 22000m, "Cá Hộp Ba Cô Gái 155g", 65 },
                    { 9, "8930000000009", 9, 48000m, "Cà Phê G7 Hòa Tan Hộp 20 Gói", 75 },
                    { 10, "8930000000010", 10, 42000m, "Bánh Bông Lan Solite Hộp 360g", 45 },
                    { 11, "8930000000011", 11, 45000m, "Kem Merino Vani Hộp 450ml", 30 },
                    { 12, "8930000000012", 12, 28000m, "Màng Bọc Thực Phẩm Ringo 30cm", 55 },
                    { 13, "8937777777777", 13, 45000m, "Lốc 6 Cuộn Giấy Vệ Sinh Bless You", 60 },
                    { 14, "8938888888888", 14, 165000m, "Dầu Gội Clear Men Bạc Hà 630g", 25 },
                    { 15, "8939999999999", 15, 38000m, "Kem Đánh Răng P/S Trà Xanh 230g", 45 },
                    { 16, "8930000000000", 16, 210000m, "Nước Giặt Omo Matic Cửa Trước 3.6kg", 15 },
                    { 17, "8930000000017", 17, 32000m, "Nước Rửa Chén Sunlight Chanh 750ml", 80 },
                    { 18, "8930000000018", 18, 18000m, "Vở Học Sinh Campus 200 Trang", 150 },
                    { 19, "8930000000019", 19, 35000m, "Khẩu Trang Y Tế Hộp 50 Cái", 100 },
                    { 20, "8930000000020", 20, 25000m, "Hộp Nhựa Đựng Thực Phẩm Duy Tân 1 Lít", 70 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
