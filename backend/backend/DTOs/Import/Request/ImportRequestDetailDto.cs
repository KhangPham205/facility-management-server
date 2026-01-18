using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class ImportRequestDetailDto
    {
        [Required]
        public string RequestId { get; set; }

        [Required]
        public string EquipmentName { get; set; }

        public int Quantity { get; set; } = 1;

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
