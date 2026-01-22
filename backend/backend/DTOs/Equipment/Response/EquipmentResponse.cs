using backend.Enums;

namespace backend.DTOs.Equipment.Response
{
    public class EquipmentResponse
    {
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string CategoryId { get; set; }
        public string? EquipmentCategoryName { get; set; }

        public string LocationId { get; set; }
        public string? LocationName { get; set; }
        public LocationType LocationType { get; set; }

        public decimal UnitPrice { get; set; }
        public EquipmentStatus Status { get; set; }
        private string? Note { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
