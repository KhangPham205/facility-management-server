using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Building.Request
{
    public class UpdateBuildingRequest
    {
        [StringLength(100)]
        public string? BuildingName { get; set; }

        [Range(0, 100)]
        public int FloorCount { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
