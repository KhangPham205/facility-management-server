using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Audit
{
    [Table("PeriodicAudits")]
    [EntityFilter(Prefix = "")]
    public class PeriodicAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PeriodId { get; set; }

        [Required]
        [StringLength(100)]
        public string PeriodicAuditName { get; set; } // VD: Kiểm kê Quý 1/2024

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string ResponsiblePerson { get; set; }


        [ForeignKey(nameof(ResponsiblePerson))]
        public User? Manager { get; set; }

        public AuditStatus Status { get; set; }

        public ICollection<InventoryAudit> InventoryAudits { get; set; }
    }
}