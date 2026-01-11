using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.InventoryAudit.Request
{
    public class InventoryAuditUpdateDto
    {
        [Required]
        public string AuditArea { get; set; } = string.Empty;

        public string? Note { get; set; }

        [Required]
        public AuditStatus Status { get; set; }

        [Required]
        public string StatusUpdatedBy { get; set; } = string.Empty;
    }
}