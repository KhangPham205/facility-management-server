using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("ImportVouchers")]
    public class ImportVoucher
    {
        [Key]
        public string ImportId { get; set; }

        [Required]
        public string SupplierId { get; set; }

        public string InvoiceId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Purpose { get; set; }

        public VoucherStatus Status { get; set; } = VoucherStatus.Pending;

        public string StatusUpdatedBy { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime? ApprovedAt { get; set; }

        // Navigation Properties
        // [ForeignKey("SupplierId")]
        // public virtual Supplier Supplier { get; set; }

        [ForeignKey("invoiceId")]
        public virtual Invoice Invoice { get; set; }

        // [ForeignKey("CreatedBy")]
        // public virtual User Creator { get; set; }
    }
}