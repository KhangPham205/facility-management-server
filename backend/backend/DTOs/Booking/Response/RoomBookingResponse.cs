using backend.Enums;

namespace backend.DTOs.Booking.Response
{
    public class RoomBookingResponse
    {
        public string BookingId { get; set; }
        public string RoomId { get; set; }
        public string RoomName { get; set; }

        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Purpose { get; set; }
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? ApprovedByName { get; set; }
        public string? Note { get; set; }
    }
}
