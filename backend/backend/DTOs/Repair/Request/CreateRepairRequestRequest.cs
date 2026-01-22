namespace backend.DTOs.Repair.Request
{
    public class CreateRepairRequestRequest
    {
        public string? Note { get; set; }
        public List<RepairRequestDetailDto> Details { get; set; }
    }
}
