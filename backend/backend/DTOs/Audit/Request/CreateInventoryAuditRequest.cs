using backend.Enums;
using backend.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.Request
{
    public class CreateInventoryAuditRequest
    {
        [StringLength(100)]
        public string AuditName { get; set; }

        public string PeriodId { get; set; }

        public string LocationId { get; set; }

        public LocationType LocationType { get; set; }

        [Required]
        public string AuditorId { get; set; }

        public DateTime AuditDate { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        public AuditStatus Status { get; set; }
    }
}
