namespace backend.DTOs.Transfer.Request
{
    public class TransferRequestDetailDto
    {
        public string EquipmentId { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
