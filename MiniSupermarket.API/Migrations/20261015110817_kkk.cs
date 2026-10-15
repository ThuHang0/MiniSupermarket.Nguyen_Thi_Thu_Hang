using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class kkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, null, "Nguyễn Quản Trị", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Admin", "admin01" },
                    { 2, null, "Trần Giám Đốc", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Admin", "admin02" },
                    { 3, null, "Lê Thu Ngân", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier01" },
                    { 4, null, "Phạm Bán Hàng", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier02" },
                    { 5, null, "Hoàng Thu Ngân", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier03" },
                    { 6, null, "Vũ Thị Quầy", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier04" },
                    { 7, null, "Đỗ Bán Lẻ", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier05" },
                    { 8, null, "Ngô Quản Kho", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Warehouse", "ware01" },
                    { 9, null, "Bùi Kiểm Kê", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Warehouse", "ware02" },
                    { 10, null, "Dương Thủ Kho", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Warehouse", "ware03" },
                    { 11, null, "Lý Nhập Hàng", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Warehouse", "ware04" },
                    { 12, null, "Đặng Hỗ Trợ", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Admin", "admin_backup" },
                    { 13, null, "Hồ Ca Chiều", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Cashier", "cashier06" },
                    { 14, null, "Trương Vận Chuyển", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Warehouse", "ware05" },
                    { 15, null, "Mai Giám Sát", true, "100000.TWluaU1hcmtldExhYjRTYW1wbGVTYWx0MjAyNg==.t1RjFaaOAyi2q+eKlNEoOiginUtF7GpXAPIPyTwH9Ig=", "Admin", "supervisor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
