using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class CreateImportVoucherRequest
    {
        [Required]
        public string RequestId { get; set; } // Link tới request đã duyệt

        // Thông tin để tạo Invoice (Hóa đơn)
        public string InvoiceId { get; set; }

        // Danh sách thiết bị nhập kho thực tế
        public List<ImportVoucherDetailDto> Details { get; set; } = new();
    }
}
