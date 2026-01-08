using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TransferVoucher
    {
        [Key]
        public string TransferId { get; set; } = Guid.NewGuid().ToString();

        public string? InvoiceId { get; set; }

        public string SourceLocation { get; set; }      // Lưu RoomId
        // [ForeignKey("SourceLocation")]
        // public virtual Room SourceRoom { get; set; }

        public string DestinationLocation { get; set; } // Lưu RoomId
        // [ForeignKey("DestinationLocation")]
        // public virtual Room DestinationRoom { get; set; }

        public string? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User? Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Reason { get; set; }

        public VoucherStatus Status { get; set; } = VoucherStatus.Pending;

        public string? StatusUpdatedBy { get; set; }
        [ForeignKey("StatusUpdatedBy")]
        public virtual User? StatusUpdater { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        public string? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public virtual User? Approver { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }
}
