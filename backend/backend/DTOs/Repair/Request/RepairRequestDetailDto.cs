namespace backend.DTOs.Repair.Request
{
    public class RepairRequestDetailDto
    {
        public string EquipmentId { get; set; }
        public string Description { get; set; } // Mô tả hư hỏng (VD: Màn hình vỡ)
    }
}
