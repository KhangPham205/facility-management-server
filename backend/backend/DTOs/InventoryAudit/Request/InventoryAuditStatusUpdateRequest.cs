using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.InventoryAudit.Request
{
    public class InventoryAuditStatusUpdateRequest
    {
        [Required]
        public AuditStatus NewStatus { get; set; }

        [Required]
        public string StatusUpdatedBy { get; set; } = string.Empty;
    }
}