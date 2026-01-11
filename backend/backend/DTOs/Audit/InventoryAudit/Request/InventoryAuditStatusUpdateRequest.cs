using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.InventoryAudit.Request
{
    public class InventoryAuditStatusUpdateRequest
    {
        [Required]
        public AuditStatus NewStatus { get; set; }

        [Required]
        public string StatusUpdatedBy { get; set; } = string.Empty;
    }
}