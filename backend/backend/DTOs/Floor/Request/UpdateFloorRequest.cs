using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Floor.Request
{
    public class UpdateFloorRequest
    {
        public string? BuildingId { get; set; }

        [StringLength(100)]
        public string? FloorName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
