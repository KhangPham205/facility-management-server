namespace backend.DTOs.Import.Response
{
    public class ImportVoucherDetailResponse
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
