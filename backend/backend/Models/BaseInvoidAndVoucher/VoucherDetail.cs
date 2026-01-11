using backend.Models.EquipmentInfo;
using backend.Models.Maintenance;
using backend.Models.Repair;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.BaseInvoidAndVoucher
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

        // Navigation Property:
        [ForeignKey("CategoryId")]
        public virtual EquipmentCategory? Category { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual Equipment? Equipment { get; set; }

        public virtual ICollection<Invoice>? Invoices { get; set; }
        public virtual ICollection<ImportVoucher>? ImportVouchers { get; set; }
        public virtual ICollection<MaintenanceVoucher>? MaintenanceVouchers { get; set; }
        public virtual ICollection<RepairVoucher>? RepairVouchers { get; set; }
        public virtual ICollection<LiquidateVoucher>? LiquidateVouchers { get; set; }
        public virtual ICollection<RepairRequest>? RepairRequests { get; set; }
        public virtual ICollection<BorrowVoucher>? BorrowVouchers { get; set; }
        public virtual ICollection<TransferVoucher>? TransferVouchers { get; set; }
    }
}