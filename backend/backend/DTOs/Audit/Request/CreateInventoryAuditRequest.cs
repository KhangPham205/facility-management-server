using backend.Enums;

namespace backend.DTOs.Audit.Request
{
    public class CreateInventoryAuditRequest
    {
        public string PeriodId { get; set; }
        public string LocationId { get; set; }
        public LocationType LocationType { get; set; }
        public string AuditorId { get; set; }
    }
}
