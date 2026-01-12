namespace backend.DTOs.Room.Request
{
    public class CreateRoomRequest
    {
        public string FloorId { get; set; }
        public string RoomName { get; set; }
        public string RoomTypeId { get; set; }
        public int Capacity { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
    }
}
