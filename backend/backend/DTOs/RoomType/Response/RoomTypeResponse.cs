using backend.Enums;

namespace backend.DTOs.RoomType.Response
{
    public class RoomTypeResponse
    {
        public string RoomTypeId { get; set; }
        public RoomTypeName TypeName { get; set; }
        public string? Description { get; set; }
    }
}