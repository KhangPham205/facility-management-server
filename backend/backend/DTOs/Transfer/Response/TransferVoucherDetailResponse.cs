namespace backend.DTOs.Transfer.Response
{
    public class TransferVoucherDetailResponse
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
