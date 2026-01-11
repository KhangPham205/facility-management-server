namespace backend.DTOs.Equipment.Request
{
    public class TransferRequestDTO
    {
        public string CreatedBy { get; set; }
        public string SourceLocation { get; set; } // RoomId cũ
        public string DestinationLocation { get; set; } // RoomId mới
        public string? Reason { get; set; }
        public List<string> EquipmentId { get; set; }
    }
}
