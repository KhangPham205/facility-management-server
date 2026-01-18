using backend.Enums;
using backend.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.Response
{
    public class InventoryAuditResponse
    {
        public string AuditId { get; set; }

        public string PeriodId { get; set; }

        // periodic audit name
        public string AuditName { get; set; }

        public string LocationId { get; set; }

        public LocationType LocationType { get; set; }

        public string LocationName { get; set; }

        public string AuditorId { get; set; }

        public string AuditorFullName { get; set; }

        public DateTime AuditDate { get; set; }

        public string? Note { get; set; }

        public AuditStatus Status { get; set; }

        public List<AuditDetailResponse> Details { get; set; }
    }
}
