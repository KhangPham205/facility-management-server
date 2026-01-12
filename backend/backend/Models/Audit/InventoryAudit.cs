using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("InventoryAudits")]
    public class InventoryAudit
    {
        [Key]
        public string AuditId { get; set; }

        public string PeriodId { get; set; }
        [ForeignKey("PeriodId")]
        public PeriodicAudit PeriodicAudit { get; set; }

        // Vị trí kiểm kê (Thường là kiểm kê theo Phòng)
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