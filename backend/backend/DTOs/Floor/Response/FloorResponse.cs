namespace backend.DTOs.Floor.Response
{
    public class FloorResponse
    {
        public string FloorId { get; set; }
        public string BuildingId { get; set; }
        public string FloorName { get; set; }
        public string? Note { get; set; }
    }
}