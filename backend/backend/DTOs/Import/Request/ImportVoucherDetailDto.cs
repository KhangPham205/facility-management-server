using backend.Enums;

namespace backend.DTOs.Import.Request
{
    public class ImportVoucherDetailDto
    {
        // Nếu nhập thêm số lượng cho thiết bị cũ -> Gửi ID
        // Nếu thiết bị mới tinh -> Để null, Backend sẽ tự tạo Equipment mới
        public string? ExistingEquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
