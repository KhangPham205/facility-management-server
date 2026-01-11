using backend.Enums;
using backend.Models.BaseInvoidAndVoucher;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LiquidateVoucher
    {
        [Key]
        public string LiquidateId { get; set; }

        [Required]
        public string InvoiceId { get; set; }

        [Required]
        public string VoucherDetailId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Reason { get; set; }

        [Required]
        public VoucherStatus Status { get; set; } = VoucherStatus.Pending;

        public string StatusUpdatedBy { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        // Navigation Properties (Quan hệ khóa ngoại)
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        [ForeignKey("VoucherDetailId")]
        public virtual VoucherDetail VoucherDetail { get; set; }
    }
}