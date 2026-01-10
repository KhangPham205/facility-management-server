using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Room.Request
{
    public class RoomInfomationUpdateRequest
    {
        public string? FloorId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomTypeId { get; set; }
        public int? Capacity { get; set; }
        public string? Note { get; set; }
    }
}