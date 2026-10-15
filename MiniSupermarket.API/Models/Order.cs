using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }

        // Ngăn lưu trùng khi gửi lại cùng yêu cầu thanh toán.
        public Guid RequestId { get; set; }

        public DateTime OrderDate { get; set; }

        public int CashierId { get; set; }

        public User? Cashier { get; set; }

        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CashReceived { get; set; }

        public List<OrderDetail> Details { get; set; } = new();
    }
}