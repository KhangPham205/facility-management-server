using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.EquipmentCategory.Request
{
    public class UpdateEquipmentCategoryRequest
    {
        [StringLength(100)]
        public string? EquipmentCategoryName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
