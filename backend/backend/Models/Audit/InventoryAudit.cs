using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Audit
{
    [Table("InventoryAudits")]
    [EntityFilter(Prefix = "")]
    public class InventoryAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AuditId { get; set; }

        [StringLength(100)]
        public string AuditName { get; set; }

        public string PeriodId { get; set; }

        public string LocationId { get; set; }

        public LocationType LocationType { get; set; }

        [Required]
        public string AuditorId { get; set; }

        public DateTime AuditDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Note { get; set; }

        public AuditStatus Status { get; set; }



        [ForeignKey("PeriodId")]
        public PeriodicAudit PeriodicAudit { get; set; }

        [ForeignKey("AuditorId")]
        public User Auditor { get; set; }

        public ICollection<AuditDetail> Details { get; set; }
    }
}