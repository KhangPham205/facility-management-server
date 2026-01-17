using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Liquidate.Request
{
    public class CreateLiquidateVoucherRequest
    {
        [Required]
        public string RequestId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        // Hóa đơn bán thanh lý (nếu có thu tiền)
        public string? InvoiceId { get; set; }

        public List<LiquidateVoucherDetailDto> Details { get; set; } = new();
    }
}
