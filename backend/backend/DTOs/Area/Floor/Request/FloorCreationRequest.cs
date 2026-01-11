using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Area.Floor.Request
{
    public class FloorCreationRequest
    {
        [Required(ErrorMessage = "FloorId là bắt buộc")]
        public string FloorId { get; set; }

        [Required(ErrorMessage = "BuildingId là bắt buộc")]
        public string BuildingId { get; set; }

        [Required(ErrorMessage = "FloorName không được để trống")]
        [StringLength(100)]
        public string FloorName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}