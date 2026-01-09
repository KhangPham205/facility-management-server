namespace backend.DTOs.FundSource
{
    public class FundSourceDto
    {
        public string SourceName { get; set; }
        public Decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
