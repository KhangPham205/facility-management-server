using backend.Enums;

namespace backend.DTOs.ImportVoucher.Response
{
    public class ImportVoucherResponse
    {
        public string ImportId { get; set; }
        public string SupplierId { get; set; }
        public string InvoiceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Purpose { get; set; }
        public VoucherStatus Status { get; set; }
        public string StatusUpdatedBy { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        // thuộc tính ngoài model gốc
        public string SupplierName {  get; set; }
        public string CreaterName { get; set; } // tên người tạo
        public string StatusUpdaterName { get; set; } // tên người updated status
        public string ApproverName { get; set; } // tên người approved
    }
}
