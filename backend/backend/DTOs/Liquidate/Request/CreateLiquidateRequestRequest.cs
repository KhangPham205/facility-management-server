namespace backend.DTOs.Liquidate.Request
{
    public class CreateLiquidateRequestRequest
    {
        public string CreatedBy { get; set; }
        public string? Note { get; set; }
        public List<LiquidateRequestDetailDto> Details { get; set; } = new();
    }
}
