using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class CreateRoomRequest
    {
        [Required]
        public string FloorId { get; set; }

        [Required]
        [StringLength(100)]
        public string RoomName { get; set; }

        [Required]
        public string RoomTypeId { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public RoomStatus Status { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
