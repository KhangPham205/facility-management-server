using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("AuditDetails")]
    [PrimaryKey(nameof(AuditId), nameof(EquipmentId))] // Định nghĩa khóa chính tổ hợp
    public class AuditDetail
    {
        [Required]
        public string AuditId { get; set; } = string.Empty;

        [Required]
        public string EquipmentId { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string EquipmentName { get; set; } = string.Empty;

        public int BookQuantity { get; set; }

        public int ActualQuantity { get; set; }

        // trường tính toán: ActualQuantity - BookQuantity
        public int Difference { get; set; }

        public string EquipmentCondition { get; set; } = string.Empty;

        public string? Note { get; set; }

        // foreign keys
        [ForeignKey("AuditId")]
        public virtual InventoryAudit? InventoryAudit { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual Equipment? Equipment { get; set; }
    }
}