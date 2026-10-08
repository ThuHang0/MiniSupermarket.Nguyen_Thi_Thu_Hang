using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Column(TypeName = "varchar(15)")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        // Gán giá trị mặc định là 0
        public int RewardPoints { get; set; } = 0;

        // Gán giá trị mặc định là 'Chuẩn'
        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}