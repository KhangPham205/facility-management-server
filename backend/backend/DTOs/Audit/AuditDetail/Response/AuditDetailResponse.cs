namespace backend.DTOs.Audit.AuditDetail.Response
{
    public class AuditDetailResponseDto
    {
        public string AuditId { get; set; } = string.Empty;
        public string EquipmentId { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public int BookQuantity { get; set; }
        public int ActualQuantity { get; set; }
        public int Difference { get; set; }
        public string EquipmentCondition { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}