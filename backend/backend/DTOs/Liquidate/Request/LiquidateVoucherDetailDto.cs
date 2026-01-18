using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Liquidate.Request
{
    public class LiquidateVoucherDetailDto
    {
        [Required]
        public string EquipmentId { get; set; }

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú cụ thể cho từng món (VD: Bán ve chai)

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LiquidatePrice { get; set; }
    }
}
