namespace backend.DTOs.Audit.Request
{
    public class UpdateAuditDetailRequest
    {
        public string DetailId { get; set; }
        public int ActualQuantity { get; set; }
        public string Condition { get; set; }
        public string Note { get; set; }
    }
}
