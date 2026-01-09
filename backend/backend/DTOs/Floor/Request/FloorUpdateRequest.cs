using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Floor.Request
{
    public class FloorUpdateRequest
    {
        [Required(ErrorMessage = "BuildingId là bắt buộc")]
        public string BuildingId { get; set; }

        [Required(ErrorMessage = "FloorName không được để trống")]
        [StringLength(100)]
        public string FloorName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}