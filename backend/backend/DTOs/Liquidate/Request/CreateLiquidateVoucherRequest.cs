namespace backend.DTOs.Liquidate.Request
{
    public class CreateLiquidateVoucherRequest
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }

        // Hóa đơn bán thanh lý (nếu có thu tiền)
        public string? InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; } // Tổng tiền thu được

        public List<LiquidateVoucherDetailDto> Details { get; set; } = new();
    }
}
