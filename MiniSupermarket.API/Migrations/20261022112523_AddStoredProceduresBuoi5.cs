using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProceduresBuoi5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Báo cáo doanh thu theo từng thu ngân.
            migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
        AS
        BEGIN
            SET NOCOUNT ON;

            SELECT
                u.Username AS CashierUsername,
                COUNT(o.Id) AS TotalOrders,
                COALESCE(SUM(o.TotalAmount), 0) AS TotalRevenue,
                COALESCE(SUM(o.RewardPoints), 0) AS TotalPointsAwarded
            FROM dbo.Orders AS o
            INNER JOIN dbo.Users AS u
                ON u.Id = o.CashierId
            GROUP BY u.Username
            ORDER BY TotalRevenue DESC, u.Username;
        END;
    ");

            // Báo cáo doanh thu theo khoảng ngày.
            migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByDateRange
            @FromDate datetime2,
            @ToDate datetime2
        AS
        BEGIN
            SET NOCOUNT ON;

            SELECT
                u.Username AS CashierUsername,
                COUNT(o.Id) AS TotalOrders,
                COALESCE(SUM(o.TotalAmount), 0) AS TotalRevenue,
                COALESCE(SUM(o.RewardPoints), 0) AS TotalPointsAwarded
            FROM dbo.Orders AS o
            INNER JOIN dbo.Users AS u
                ON u.Id = o.CashierId
            WHERE o.OrderDate >= @FromDate
                AND o.OrderDate < @ToDate
            GROUP BY u.Username
            ORDER BY TotalRevenue DESC, u.Username;
        END;
    ");

            // Thống kê tối đa 5 sản phẩm bán nhiều nhất.
            migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE dbo.sp_GetTop5SellingProducts
        AS
        BEGIN
            SET NOCOUNT ON;

            SELECT TOP (5)
                p.ProductId,
                p.ProductName,
                SUM(CAST(d.Quantity AS bigint)) AS TotalQuantity,
                SUM(d.Quantity * d.UnitPrice) AS TotalRevenue
            FROM dbo.OrderDetails AS d
            INNER JOIN dbo.Products AS p
                ON p.ProductId = d.ProductId
            GROUP BY p.ProductId, p.ProductName
            ORDER BY TotalQuantity DESC, p.ProductId;
        END;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DROP PROCEDURE IF EXISTS dbo.sp_GetTop5SellingProducts;");

            migrationBuilder.Sql(
                "DROP PROCEDURE IF EXISTS dbo.sp_GetRevenueByDateRange;");

            migrationBuilder.Sql(
                "DROP PROCEDURE IF EXISTS dbo.sp_GetRevenueByCashier;");
        }
    }
}
