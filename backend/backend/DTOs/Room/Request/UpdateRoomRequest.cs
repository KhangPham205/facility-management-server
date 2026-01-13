using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class UpdateRoomRequest
    {
        public string FloorId { get; set; }

        [StringLength(100)]
        public string RoomName { get; set; }

        public string RoomTypeId { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public string Status { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
