using backend.Enums;

namespace backend.DTOs.Room.Response
{
    public class RoomResponse
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public string FloorId { get; set; }
        public string FloorName { get; set; }
        public string RoomTypeId { get; set; }
        public string TypeName { get; set; }
        public int Capacity { get; set; }
        public RoomStatus? Status { get; set; }
        public string? Note { get; set; }
    }
}
