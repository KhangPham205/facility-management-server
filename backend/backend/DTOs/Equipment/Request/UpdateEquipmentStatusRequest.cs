using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Equipment.Request
{
    public class UpdateEquipmentStatusRequest
    {
        [Required]
        public EquipmentStatus Status { get; set; }
    }
}
