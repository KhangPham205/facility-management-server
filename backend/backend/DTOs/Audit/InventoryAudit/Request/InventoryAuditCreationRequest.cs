using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.InventoryAudit.Request
{
    public class InventoryAuditCreationRequest
    {
        [Required]
        public string PeriodId { get; set; } = string.Empty;

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public string AuditArea { get; set; } = string.Empty;

        public string? Note { get; set; }
    }
}