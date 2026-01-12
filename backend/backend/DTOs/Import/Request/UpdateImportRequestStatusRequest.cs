using backend.Enums;

namespace backend.DTOs.Import.Request
{
    public class UpdateImportRequestStatusRequest
    {
        public VoucherStatus Status { get; set; } // Approved / Rejected
        public string ApprovedBy { get; set; }
    }
}
