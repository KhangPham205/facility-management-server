namespace backend.DTOs.Floor.Response
{
    public class FloorResponse
    {
        public string FloorId { get; set; }
        public string BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string? Note { get; set; }

        // thuộc tính ngoài model
        public string FloorName { get; set; }
    }
}