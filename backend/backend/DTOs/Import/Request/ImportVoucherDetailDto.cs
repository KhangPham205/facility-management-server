using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class ImportVoucherDetailDto
    {
        [Required]
        [StringLength(100)]
        public string EquipmentName { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
