using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Building.Request
{
    public class UpdateBuildingRequest
    {
        [Required]
        [StringLength(100)]
        public string? BuildingName { get; set; }

        [Required]
        [Range(0, 100)]
        public int FloorCount { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
