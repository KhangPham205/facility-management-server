using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Area
{
    [Table("RoomBookings")]
    public class RoomBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BookingId { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public string BorrowerId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(500)]
        public string Purpose { get; set; }     // Mục đích mượn (Họp, Dạy học...)

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? Note { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("BorrowerId")]
        public User Borrower { get; set; }

        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }
    }
}