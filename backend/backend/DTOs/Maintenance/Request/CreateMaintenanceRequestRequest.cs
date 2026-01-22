namespace backend.DTOs.Maintenance.Request
{
    public class CreateMaintenanceRequestRequest
    {
        public string? Note { get; set; }
        public List<MaintenanceRequestDetailDto> Details { get; set; } = new();
    }
}
