using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    public class LiquidateRequest
    {
        [Key]
        public string RequestId { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public VoucherStatus Status { get; set; }

        public string? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public ICollection<LiquidateRequestDetail> Details { get; set; }
    }
}
