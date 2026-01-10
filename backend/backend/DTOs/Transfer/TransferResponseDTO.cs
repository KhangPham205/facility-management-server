namespace backend.DTOs.Transfer
{
    public class TransferResponseDTO
    {
        public string TransferId { get; set; }
        public string EquipmentId { get; set; }
        public string SourceLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
