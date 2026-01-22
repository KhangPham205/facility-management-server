using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Response
{
    public class ImportRequestResponse
    {
        public string RequestId { get; set; }
        public string? CreatedByName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ApprovedByName { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<ImportRequestDetailResponse> Details { get; set; }
    }
}
