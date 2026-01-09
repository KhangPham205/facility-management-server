using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class RoomUpdateRequest
    {
        [Required]
        public string FloorId { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string RoomTypeId { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
    }
}