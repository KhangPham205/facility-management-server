using backend.Enums;
using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Audit
{
    [Table("AuditDetails")]
    [PrimaryKey(nameof(AuditId), nameof(EquipmentId))]
    public class AuditDetail
    {
        [Required]
        public string AuditId { get; set; }

        [Required]
        public string EquipmentId { get; set; }


        //public int BookQuantity { get; set; }
        //public int ActualQuantity { get; set; }

        //public int Difference { get; set; }

        public EquipmentStatus? Condition { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }


        [ForeignKey(nameof(AuditId))]
        public InventoryAudit InventoryAudit { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment Equipment { get; set; }
    }
}