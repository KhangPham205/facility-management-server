namespace backend.Enums
{
    public enum BookingStatus
    {
        Pending = 0,    // Chờ duyệt
        Approved = 1,   // Đã duyệt (Phòng đã được đặt)
        Rejected = 2,   // Từ chối
        Cancelled = 3,  // Người dùng tự hủy
        Completed = 4   // Đã sử dụng xong
    }
}