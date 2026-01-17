using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace backend.DTOs.Equipment.Request
{
    public class UpdateEquipmentRequest
    {
        [StringLength(100)]
        public string? EquipmentName { get; set; }
        public string? CategoryId { get; set; }
        public string? LocationId { get; set; }
        public LocationType LocationType { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPublic { get; set; }
        public EquipmentStatus Status { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
    }
}
