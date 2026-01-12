using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    public class LiquidateVoucherDetail
    {
        // Composite Key
        public string LiquidateId { get; set; }
        public string EquipmentId { get; set; }

        public string? Note { get; set; }
        public decimal LiquidatePrice { get; set; } // Giá thanh lý thực tế

        [ForeignKey("LiquidateId")]
        public LiquidateVoucher LiquidateVoucher { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
