namespace backend.DTOs.Equipment.Request
{
    public class CreateEquipmentDTO
    {
        public string EquipmentName { get; set; }
        public string CategoryId { get; set; }
        public string? RoomId { get; set; }
        public int Quantity { get; set; }
        public bool IsPublic { get; set; }
        public string? Description { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
    }
}
