using backend.DTOs.Repair.Request;

namespace backend.DTOs.Maintenance.Request
{
    public class CreateMaintenanceVoucherRequest
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string InvoiceId { get; set; }
        public List<MaintenanceRequestDetailDto> Details { get; set; }
    }
}
