namespace backend.DTOs.Area.Building.Response
{
    public class BuildingResponse
    {
        public string BuildingId { get; set; }
        public string BuildingName { get; set; }
        public int FloorCount { get; set; }
        public string? Note { get; set; }
    }
}