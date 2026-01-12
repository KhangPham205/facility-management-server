using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Borrow
{
    public class BorrowVoucher
    {
        [Key]
        public string BorrowId { get; set; }

        public string BorrowerId { get; set; }
        [ForeignKey("BorrowerId")]
        public User Borrower { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Purpose { get; set; }
        public BorrowStatus Status { get; set; }

        public string? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public DateTime? ReturnDate { get; set; } // Ngày trả thực tế

        public ICollection<BorrowVoucherDetail> Details { get; set; }
    }
}