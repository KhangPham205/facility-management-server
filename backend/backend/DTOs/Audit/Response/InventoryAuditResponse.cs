namespace backend.DTOs.Audit.Response
{
    public class InventoryAuditResponse
    {
        public string AuditId { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
        public List<AuditDetailResponse> Details { get; set; }
    }
}
