using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("PeriodicAudits")]
    public class PeriodicAudit
    {
        [Key]
        public string PeriodId { get; set; } = string.Empty;

        [Required]
        public AuditPeriodType AuditType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(255)]
        public string ResponsiblePerson { get; set; } = string.Empty;
    }
}