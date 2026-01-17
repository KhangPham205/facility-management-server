using backend.Enums;

namespace backend.DTOs.Maintenance.Response
{
    public class MaintenanceRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<MaintenanceRequestDetailResponse> Details { get; set; }
    }
}
