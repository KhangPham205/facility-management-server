using backend.Enums;

namespace backend.DTOs.Booking.Request
{
    public class UpdateBookingStatusRequest
    {
        public BookingStatus Status { get; set; }

        public string? Note { get; set; }
    }
}
