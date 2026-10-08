using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8931111111111", 1, 12000m, "Snack Khoai Tây Lay's Vị Tự Nhiên", 50 },
                    { 2, "8932222222222", 2, 10000m, "Nước Ngọt Coca-Cola Lon 320ml", 120 },
                    { 3, "8933333333333", 3, 8500m, "Sữa Tươi Tiệt Trùng TH True Milk 180ml", 200 },
                    { 4, "8934444444444", 4, 4500m, "Mì Hảo Hảo Tôm Chua Cay", 500 },
                    { 5, "8935555555555", 5, 25000m, "Nước Mắm Nam Ngư Đệ Nhị 900ml", 30 },
                    { 6, "8936666666666", 6, 55000m, "Dầu Ăn Simply Đậu Nành 1 Lít", 40 },
                    { 7, "8937777777777", 13, 45000m, "Lốc 6 Cuộn Giấy Vệ Sinh Bless You", 60 },
                    { 8, "8938888888888", 14, 165000m, "Dầu Gội Clear Men Bạc Hà 630g", 25 },
                    { 9, "8939999999999", 15, 38000m, "Kem Đánh Răng P/S Trà Xanh 230g", 45 },
                    { 10, "8930000000000", 16, 210000m, "Nước Giặt Omo Matic Cửa Trước 3.6kg", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);
        }
    }
}
