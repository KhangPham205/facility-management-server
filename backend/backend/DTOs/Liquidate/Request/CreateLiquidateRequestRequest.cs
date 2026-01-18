using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Liquidate.Request
{
    public class CreateLiquidateRequestRequest
    {
        [StringLength(500)]
        public string? Note { get; set; }
        public List<LiquidateRequestDetailDto> Details { get; set; } = new();
    }
}
