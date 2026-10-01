using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát", "Nước ngọt, nước suối, trà đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Đồ uống dinh dưỡng", "Sữa tươi, sữa hộp, sữa dinh dưỡng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì & Thực phẩm ăn liền", "Mì gói, cháo ăn liền, phở ăn liền" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị nấu ăn", "Nước mắm, nước tương, muối, đường" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });
        }
    }
}
