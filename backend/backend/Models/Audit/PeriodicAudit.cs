using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Audit
{
    [Table("PeriodicAudits")]
    public class PeriodicAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PeriodId { get; set; }

        [Required]
        [StringLength(100)]
        public string AuditName { get; set; } // VD: Kiểm kê Quý 1/2024

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string ResponsiblePerson { get; set; }


        [ForeignKey(nameof(ResponsiblePerson))]
        public User? Manager { get; set; }

        public AuditStatus Status { get; set; }

        public ICollection<InventoryAudit> InventoryAudits { get; set; }
    }
}