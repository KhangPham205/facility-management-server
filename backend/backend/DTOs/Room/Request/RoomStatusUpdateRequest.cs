using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class RoomStatusUpdateRequest
    {
        [Required]
        public string Status { get; set; }
        public string? Note { get; set; }
    }
}