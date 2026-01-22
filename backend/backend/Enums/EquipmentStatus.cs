namespace backend.Enums
{
    public enum EquipmentStatus
    {
        Unassigned = 0,     // Mới nhập, chưa xử lý
        Available = 1,      // Khả dụng (Sẵn sàng cho mượn)
        Borrowed = 2,       // Đang cho mượn
        UnderMaintenance = 3, // Đang bảo trì
        Broken = 4,         // Hỏng (Cần sửa chữa)
        Lost = 5,           // Thất lạc
        Disposed = 6        // Đã thanh lý
    }
}
