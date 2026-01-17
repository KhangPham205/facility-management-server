using backend.Enums;

namespace backend.DTOs.Audit.Response
{
    public class AuditDetailResponse
    {
        public string DetailId { get; set; }
        public string EquipmentName { get; set; }
        public EquipmentStatus? Condition { get; set; }
        public string? Note { get; set; }
    }
}
