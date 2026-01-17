using backend.Enums;
using backend.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.Request
{
    public class CreateInventoryAuditRequest
    {
        public string PeriodId { get; set; }

        public string LocationId { get; set; }

        public LocationType LocationType { get; set; }

        [Required]
        public User Auditor { get; set; }

        public DateTime AuditDate { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        public AuditStatus Status { get; set; }
    }
}
