namespace backend.DTOs.Liquidate.Response
{
    public class LiquidateVoucherResponse
    {
        public string LiquidateId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; } // Tiền thu về
        public List<LiquidateVoucherDetailResponse> Details { get; set; }
    }
}
