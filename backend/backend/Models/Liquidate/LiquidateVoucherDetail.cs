using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    [Table("LiquidateVoucherDetails")]
    public class LiquidateVoucherDetail
    {
        // Composite Key
        [Required]
        public string LiquidateId { get; set; }

        [Required]
        public string EquipmentId { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [Required]
        public decimal LiquidatePrice { get; set; } = 0;// Giá thanh lý thực tế


        [ForeignKey("LiquidateId")]
        public LiquidateVoucher LiquidateVoucher { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
