using backend.Enums;

namespace backend.DTOs.Equipment.Request
{
    public class CreateEquipmentRequest
    {
        public string? EquipmentName { get; set; }
        public string? CategoryId { get; set; }
        public string? LocationId { get; set; }
        public LocationType LocationType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPublic { get; set; }
        public EquipmentStatus Status { get; set; }
        public string? Description { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
    }
}
