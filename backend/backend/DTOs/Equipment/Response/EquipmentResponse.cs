using backend.Enums;

namespace backend.DTOs.Equipment.Response
{
    public class EquipmentResponse
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string CategoryName { get; set; }

        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public LocationType LocationType { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public EquipmentStatus Status { get; set; }
        public string Image { get; set; }
    }
}
