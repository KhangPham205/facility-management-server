using backend.Enums;

namespace backend.DTOs.Audit.InventoryAudit.Response
{
    public class InventoryAuditResponse
    {
        public string AuditId { get; set; } = string.Empty;
        public string PeriodId { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string AuditArea { get; set; } = string.Empty;
        public string? Note { get; set; }
        public AuditStatus Status { get; set; } = AuditStatus.Waiting;
        public string StatusUpdatedBy { get; set; } = string.Empty;
        public DateTime? StatusUpdatedAt { get; set; }


        public string CreaterName { get; set; } = string.Empty;
        public string PeriodType { get; set; } = string.Empty;
        public string StatusUpdater { get; set; } = string.Empty;

    }
}