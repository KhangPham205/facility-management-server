using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Equipment
    {
        [Key]
        public string EquipmentId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EquipmentName { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public bool IsPublic { get; set; } = false;

        [Required]
        public string CategoryId { get; set; }

        public string? RoomId { get; set; }

        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

        // Thông tin bảo hành/bảo trì
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Foreign key
        [ForeignKey("CategoryId")]
        public virtual EquipmentCategory? Category { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room? Room { get; set; }

    }
}
