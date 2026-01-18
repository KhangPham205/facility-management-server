using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Booking.Request
{
    public class CreateBookingRequest
    {
        [Required]
        public string RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(500)]
        public string Purpose { get; set; }
    }
}
