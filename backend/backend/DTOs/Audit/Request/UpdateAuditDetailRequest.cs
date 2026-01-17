using backend.Enums;

namespace backend.DTOs.Audit.Request
{
    public class UpdateAuditDetailRequest
    {
        public string DetailId { get; set; }
        public EquipmentStatus Condition { get; set; }
        public string Note { get; set; }
    }
}
