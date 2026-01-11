using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("VoucherDetails")]
    public class VoucherDetail
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string VoucherDetailId { get; set; }

        [Required]
        [StringLength(50)]
        public string EquipmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string EquipmentName { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        // Navigation Property: Một chi tiết chứng từ có thể có nhiều hóa đơn (theo sơ đồ)
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}