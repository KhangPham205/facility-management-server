using backend.Enums;

namespace backend.DTOs.LiquidateVoucher.Response
{
    public class LiquidateVoucherResponse
    {
        public string LiquidateId { get; set; }
        public string InvoiceId { get; set; }
        public string VoucherDetailId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reason { get; set; }
        public VoucherStatus Status { get; set; } 
        public string StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
    }
}