namespace backend.DTOs.Liquidate.Response
{
    public class LiquidateRequestDetailResponse
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
