using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Area.Building.Request
{
    public class BuildingUpdateRequest
    {
        [Required(ErrorMessage = "Building Name is required.")]
        [StringLength(100, ErrorMessage = "Building Name cannot exceed 100 characters.")]
        public string BuildingName { get; set; }

        [Required(ErrorMessage = "Floor Count is required.")]
        [Range(1, 100, ErrorMessage = "Floor Count must be between 1 and 100.")]
        public int FloorCount { get; set; }

        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string? Note { get; set; }
    }
}