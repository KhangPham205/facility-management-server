namespace backend.DTOs.Audit.Request
{
    public class CreateAuditPeriodRequest
    {
        public string AuditName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
