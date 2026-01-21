using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class UpdateRoomStatusRequest
    {
        [Required]
        public RoomStatus Status { get; set; }


    }
}
