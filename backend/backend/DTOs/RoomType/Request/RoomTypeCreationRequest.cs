using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.RoomType.Request
{
    public class RoomTypeCreationRequest
    {
        [Required]
        [StringLength(100)]
        public RoomTypeName TypeName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}