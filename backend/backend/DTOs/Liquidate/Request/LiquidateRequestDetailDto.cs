namespace backend.DTOs.Liquidate.Request
{
    public class LiquidateRequestDetailDto
    {
        public string EquipmentId { get; set; }
        public int Quantity { get; set; } // Số lượng muốn thanh lý
    }
}
