namespace backend.DTOs.Building.Request
{
    public class CreateBuildingRequest
    {
        public string BuildingName { get; set; }
        public int FloorCount { get; set; }
        public string? Note { get; set; }
    }
}
