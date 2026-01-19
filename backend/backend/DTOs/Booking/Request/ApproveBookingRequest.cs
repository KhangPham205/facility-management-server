namespace backend.DTOs.Booking.Request
{
    public class ApproveBookingRequest
    {
        public bool IsApproved { get; set; } // True: Duyệt, False: Từ chối
        public string? Note { get; set; }    // Lý do
    }
}
