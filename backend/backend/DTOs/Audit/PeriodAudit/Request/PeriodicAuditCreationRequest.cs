using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.PeriodAudit.Request
{
    public class PeriodicAuditCreationRequest
    {
        [Required]
        [EnumDataType(typeof(AuditPeriodType))]
        public AuditPeriodType AuditType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ResponsiblePerson { get; set; } = string.Empty;
    }
}