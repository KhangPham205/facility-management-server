using backend.Enums;

namespace backend.DTOs.Import.Response
{
    public class ImportRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; } // Tên người tạo
        public DateTime CreatedAt { get; set; }
        public string Reason { get; set; }
        public VoucherStatus Status { get; set; }
        public List<ImportRequestDetailResponse> Details { get; set; }
    }
}
