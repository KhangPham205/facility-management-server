using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Area.Room.Request
{
    public class RoomCreationRequest
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