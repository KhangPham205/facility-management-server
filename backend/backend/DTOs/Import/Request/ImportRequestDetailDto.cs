using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class ImportRequestDetailDto
    {
        [Required]
        [StringLength(100)]
        public string EquipmentName { get; set; }

        public int Quantity { get; set; } = 1;

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
