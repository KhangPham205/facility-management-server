using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Import
{
    public class ImportRequest
    {
        [Key]
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Reason { get; set; }
        public VoucherStatus Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public ICollection<ImportRequestDetail> Details { get; set; }
    }
}
