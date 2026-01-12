using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("AuditDetails")]
    [PrimaryKey(nameof(AuditId), nameof(EquipmentId))]
    public class AuditDetail
    {
        public string AuditId { get; set; }
        [ForeignKey(nameof(AuditId))]
        public InventoryAudit InventoryAudit { get; set; }

        public string EquipmentId { get; set; }
        [ForeignKey(nameof(EquipmentId))]
        public Equipment Equipment { get; set; }

        public int BookQuantity { get; set; }
        public int ActualQuantity { get; set; }

        public int Difference { get; set; }

        public string? Condition { get; set; }
        public string? Note { get; set; }
    }
}