namespace MiniSupermarket.API.Models;
public class CashierRevenueDto
{
    public string CashierUsername { get; set; } = "";
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalPointsAwarded { get; set; }
}
