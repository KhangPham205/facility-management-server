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

        public string LocationId { get; set; }

        public LocationType LocationType { get; set; }

        [Required]
        public User Auditor { get; set; }

        public DateTime AuditDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Note { get; set; }

        public AuditStatus Status { get; set; }



        [ForeignKey("PeriodId")]
        public PeriodicAudit PeriodicAudit { get; set; }

        public string AuditorId { get; set; }
        [ForeignKey("AuditorId")]

        public ICollection<AuditDetail> Details { get; set; }
    }
}