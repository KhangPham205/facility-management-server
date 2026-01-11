using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Audit.AuditDetail.Request
{
    public class AuditDetailUpdateRequest
    {
        [Required]
        public int ActualQuantity { get; set; }

        [Required]
        public string EquipmentCondition { get; set; } = string.Empty;

        public string? Note { get; set; }
    }
}