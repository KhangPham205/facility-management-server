namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceRequestDetailDto
    {
        public string EquipmentId { get; set; }
        public string Quantity { get; set; }
        public string? Note { get; set; } // Mô tả tình trạng (VD: Máy chạy chậm)
    }
}
