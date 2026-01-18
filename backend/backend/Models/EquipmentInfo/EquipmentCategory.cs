using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.EquipmentInfo
{
    [Table("EquipmentCategories")]
    public class EquipmentCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? EquipmentCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string? EquipmentCategoryName { get; set; }


        [StringLength(500)]
        public string? Note { get; set; }

        public ICollection<Criteria>? Criterias { get; set; }
        public ICollection<Equipment>? Equipments { get; set; }
    }
}
