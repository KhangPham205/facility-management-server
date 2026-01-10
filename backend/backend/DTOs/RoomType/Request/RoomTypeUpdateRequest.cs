using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.DTOs.RoomType.Request
{
   public class RoomTypeUpdateRequest
    {
        [Required]
        [StringLength(100)]
        public RoomTypeName TypeName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}