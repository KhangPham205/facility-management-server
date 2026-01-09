namespace backend.Enums
{
    public enum EquipmentStatus
    {
        Available = 0,      // Khả dụng (Sẵn sàng cho mượn)
        Borrowed = 1,       // Đang cho mượn
        UnderMaintenance = 2, // Đang bảo trì
        Broken = 3,         // Hỏng (Cần sửa chữa)
        Lost = 4,           // Thất lạc
        Disposed = 5        // Đã thanh lý
    }
}
