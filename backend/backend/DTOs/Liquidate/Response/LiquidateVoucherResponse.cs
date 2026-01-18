namespace backend.DTOs.Liquidate.Response
{
    public class LiquidateVoucherResponse
    {
        public string LiquidateId { get; set; }
        public string RequestId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; } // Tiền thu về
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorFullName { get; set; }
        public List<LiquidateVoucherDetailResponse> Details { get; set; }
    }
}
