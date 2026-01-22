using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Floor.Request
{
    public class CreateFloorRequest
    {
        [Required]
        public string? BuildingId { get; set; }

        [Required]
        [StringLength(100)]
        public string? FloorName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
