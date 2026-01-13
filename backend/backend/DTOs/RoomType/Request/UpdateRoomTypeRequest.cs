using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.RoomType.Request;

public class UpdateRoomTypeRequest
{
    [Required]
    [StringLength(100)]
    public string? TypeName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}