namespace backend.DTOs.Import.Request
{
    public class CreateImportVoucherRequest
    {
        public string RequestId { get; set; } // Link tới request đã duyệt
        public string CreatedBy { get; set; }

        // Thông tin để tạo Invoice (Hóa đơn)
        public string InvoiceId { get; set; }

        // Danh sách thiết bị nhập kho thực tế
        public List<ImportVoucherDetailDto> Details { get; set; } = new();
    }
}
