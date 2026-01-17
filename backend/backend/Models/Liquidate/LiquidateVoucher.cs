using backend.Models.Finance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    [Table("LiquidateVouchers")]
    public class LiquidateVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LiquidateId { get; set; }

        [Required]
        public string RequestId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? InvoiceId { get; set; }



        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        [ForeignKey("RequestId")]
        public LiquidateRequest Request { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }

        public ICollection<LiquidateVoucherDetail> Details { get; set; }
    }
}
