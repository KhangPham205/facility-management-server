using backend.DTOs.Floor.Response;

namespace backend.DTOs.Building.Response
{
    public class BuildingResponse
    {
        public string BuildingId { get; set; }
        public string BuildingName { get; set; }
        public int FloorCount { get; set; }
        public string? Note { get; set; }
        // Có thể trả về danh sách Floor tóm tắt nếu cần
        public ICollection<FloorResponse> Floors { get; set; }
    }
}
