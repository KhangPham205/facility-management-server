namespace backend.DTOs.FundSource.Response
{
    public class FundSourceResponse
    {
        public string SourceId { get; set; }
        public string SourceName { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
