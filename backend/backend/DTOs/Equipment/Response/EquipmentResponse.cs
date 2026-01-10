using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Equipment.Response
{
    public class EquipmentResponse
    {
        public string EquipmentId { get; set; } = Guid.NewGuid().ToString();
        public string EquipmentName { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsPublic { get; set; } = false;
        public string CategoryId { get; set; }
        public string? RoomId { get; set; }

        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

        // Thông tin bảo hành/bảo trì
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}