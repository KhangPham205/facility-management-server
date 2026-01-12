using backend.Models.Finance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    public class LiquidateVoucher
    {
        [Key]
        public string LiquidateId { get; set; }

        public string RequestId { get; set; }
        [ForeignKey("RequestId")]
        public LiquidateRequest Request { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }

        public ICollection<LiquidateVoucherDetail> Details { get; set; }
    }
}
