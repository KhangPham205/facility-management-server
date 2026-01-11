using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("InventoryAudits")]
    public class InventoryAudit
    {
        [Key]
        public string AuditId { get; set; } = string.Empty;

        [Required]
        public string PeriodId { get; set; } = string.Empty;

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(255)]
        public string AuditArea { get; set; } = string.Empty;

        public string? Note { get; set; }

        [Required]
        public AuditStatus Status { get; set; } = AuditStatus.Waiting;

        public string StatusUpdatedBy { get; set; } = string.Empty;

        public DateTime? StatusUpdatedAt { get; set; }


        // navigation properties

        [ForeignKey("PeriodId")]
        public virtual PeriodicAudit? PeriodicAudit { get; set; }

        public virtual ICollection<InventoryAudit> InventoryAudits { get; set; }

    }
}