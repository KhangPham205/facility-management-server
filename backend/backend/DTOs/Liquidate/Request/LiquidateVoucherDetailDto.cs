namespace backend.DTOs.Liquidate.Request
{
    public class LiquidateVoucherDetailDto
    {
        public string EquipmentId { get; set; }
        public string? Note { get; set; } // Ghi chú cụ thể cho từng món (VD: Bán ve chai)
    }
}
