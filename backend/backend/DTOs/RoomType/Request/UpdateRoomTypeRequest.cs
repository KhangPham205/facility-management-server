using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.RoomType.Request;

public class UpdateRoomTypeRequest
{
    [StringLength(100)]
    public string? TypeName { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }
}