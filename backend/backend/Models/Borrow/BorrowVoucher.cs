using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Borrow
{
    [Table("BorrowVouchers")]
    public class BorrowVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BorrowId { get; set; }

        public string BorrowerId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public BorrowStatus Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? ReturnDate { get; set; } // Ngày trả thực tế

        [ForeignKey("BorrowerId")]
        public User Borrower { get; set; }

        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }

        public ICollection<BorrowVoucherDetail> Details { get; set; }
    }
}