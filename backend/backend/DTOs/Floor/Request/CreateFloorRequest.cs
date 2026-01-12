namespace backend.DTOs.Floor.Request
{
    public class CreateFloorRequest
    {
        public string BuildingId { get; set; }
        public string FloorName { get; set; }
        public int RoomCount { get; set; }
        public string? Note { get; set; }
    }
}
