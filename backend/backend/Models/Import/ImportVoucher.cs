using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("ImportVouchers")]
    public class ImportVoucher
    {
        [Key]
        public string importId { get; set; }

        [Required]
        public string supplierId { get; set; }

        public string invoiceId { get; set; }

        [Required]
        public string createdBy { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public string purpose { get; set; }

        public VoucherStatus status { get; set; } = VoucherStatus.Pending;

        public string statusUpdatedBy { get; set; }

        public DateTime? statusUpdatedAt { get; set; }

        public string approvedBy { get; set; }

        public DateTime? approvedAt { get; set; }

        // Navigation Properties (Nếu bạn có các bảng liên quan)
        // [ForeignKey("SupplierId")]
        // public virtual Supplier Supplier { get; set; }

        // [ForeignKey("CreatedBy")]
        // public virtual User Creator { get; set; }
    }
}