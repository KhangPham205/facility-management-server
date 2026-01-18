using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Liquidate.Request
{
    public class LiquidateRequestDetailDto
    {
        [Required]
        public string EquipmentId { get; set; }

        [StringLength(500)]
        public string? Note { get; set; } // Ghi chú cụ thể cho từng món (VD: Bán ve chai)
    }
}
