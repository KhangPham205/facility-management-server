namespace backend.DTOs.Maintenance.Request
{
    public class CreateMaintenanceRequestRequest
    {
        public string CreatedBy { get; set; }
        public string? Note { get; set; }
        public List<MaintenanceRequestDetailDto> Details { get; set; } = new();
    }
}
