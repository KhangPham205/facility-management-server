using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BorrowVoucher
    {
        [Key]
        public string BorrowId { get; set; } = Guid.NewGuid().ToString();

        // BorrowerId trỏ tới User
        public string BorrowerId { get; set; }
        [ForeignKey("BorrowerId")]
        public virtual User? Borrower { get; set; }

        public string? Purpose { get; set; }

        public BorrowStatus Status { get; set; } = BorrowStatus.Pending;

        // Log trạng thái
        public string? StatusUpdatedBy { get; set; }
        [ForeignKey("StatusUpdatedBy")]
        public virtual User? StatusUpdater { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        // Quan hệ 1-Nhiều với chi tiết (để code logic hoạt động)
        public virtual ICollection<BorrowDetail>? BorrowDetails { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}