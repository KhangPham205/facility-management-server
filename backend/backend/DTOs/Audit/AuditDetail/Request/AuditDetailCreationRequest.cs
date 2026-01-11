using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.AuditDetail.Request
{
    public class AuditDetailCreationRequest
    {
        [Required]
        public string EquipmentId { get; set; } = string.Empty;

        [Required]
        public string EquipmentName { get; set; } = string.Empty;

        public int BookQuantity { get; set; }

        public string? Note { get; set; }
    }
}