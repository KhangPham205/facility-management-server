using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Audit
{
    [Table("InventoryAudits")]
    public class InventoryAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AuditId { get; set; }

        public string PeriodId { get; set; }
        [ForeignKey("PeriodId")]
        public PeriodicAudit PeriodicAudit { get; set; }

        public string LocationId { get; set; }
        public LocationType LocationType { get; set; }

        public string AuditorId { get; set; }
        [ForeignKey("AuditorId")]
        public User Auditor { get; set; }

        public DateTime AuditDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public AuditStatus Status { get; set; }

        public ICollection<AuditDetail> Details { get; set; }
    }
}