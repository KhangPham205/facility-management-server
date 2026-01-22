using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Building.Request
{
    public class UpdateBuildingRequest
    {
        [StringLength(100)]
        public string? BuildingName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
