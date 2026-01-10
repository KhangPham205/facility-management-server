namespace backend.DTOs.Audit.Response
{
    public class PeriodicAuditResponseDto
    {
        public string PeriodId { get; set; } = string.Empty;
        public string AuditType { get; set; } = string.Empty; // Trả về "Monthly", "Quarterly",...
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ResponsiblePerson { get; set; } = string.Empty;
    }
);
}