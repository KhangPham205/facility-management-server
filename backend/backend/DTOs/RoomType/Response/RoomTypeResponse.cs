using backend.DTOs.Room.Response;

namespace backend.DTOs.RoomType.Response;

public class RoomTypeResponse
{
    public string? RoomTypeId { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }

    public ICollection<RoomResponse> Rooms { get; set; } = new List<RoomResponse>();
}