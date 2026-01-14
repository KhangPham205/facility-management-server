namespace backend.DTOs.Liquidate.Response
{
    public class LiquidateVoucherResponse
    {
        public string LiquidateId { get; set; }
        public string RequestId { get; set; }
        private string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; } // Tiền thu về
        public List<LiquidateVoucherDetailResponse> Details { get; set; }
    }
}
