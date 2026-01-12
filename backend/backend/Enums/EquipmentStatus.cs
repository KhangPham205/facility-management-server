namespace backend.Enums
{
    public enum EquipmentStatus
    {
        Unassigned,     // Mới nhập, chưa xử lý
        Available,      // Khả dụng (Sẵn sàng cho mượn)
        Borrowed,       // Đang cho mượn
        UnderMaintenance, // Đang bảo trì
        Broken,         // Hỏng (Cần sửa chữa)
        Lost,           // Thất lạc
        Disposed        // Đã thanh lý
    }
}
