using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.EquipmentInfo
{
    [Table("Equipments")]
    public class Equipment
    {
        [Key]
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string CategoryId { get; set; }

        public string LocationId { get; set; }
        public LocationType LocationType { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPublic { get; set; }
        public EquipmentStatus Status { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(CategoryId))]
        public EquipmentCategory Category { get; set; }
    }
}
