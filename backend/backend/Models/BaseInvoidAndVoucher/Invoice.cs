using backend.Enums;
using backend.Models.Maintenance;
using backend.Models.Repair;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.BaseInvoidAndVoucher
{
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string VoucherDetailId { get; set; }

        [Required]
        public FunctionType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // foreign key
        [ForeignKey("VoucherDetailId")]
        public virtual VoucherDetail? VoucherDetail { get; set; }

        public virtual ICollection<ImportVoucher>? ImportVouchers { get; set; }
        public virtual ICollection<MaintenanceVoucher>? MaintenanceVouchers { get; set;}
        public virtual ICollection<RepairVoucher>? RepairVouchers { get; set; }
        public virtual ICollection<LiquidateVoucher>? LiquidateVouchers { get;set; }
    }
}