namespace backend.DTOs.Floor.Response
{
    public class FloorResponse
    {
        public string FloorId { get; set; }
        public string BuildingId { get; set; }
        public string BuildingName { get; set; } // Map từ Building
        public string FloorName { get; set; }
        public int RoomCount { get; set; }
        public string? Note { get; set; }
    }
}
