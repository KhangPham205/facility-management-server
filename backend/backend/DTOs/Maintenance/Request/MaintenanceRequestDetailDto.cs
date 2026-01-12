namespace backend.DTOs.Maintenance.Request
{
    public class MaintenanceRequestDetailDto
    {
        public string EquipmentId { get; set; }
        public string Description { get; set; } // Mô tả tình trạng (VD: Máy chạy chậm)
    }
}
