using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.EquipmentInfo
{
    [Table("Criterias")]
    public class Criteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CriteriaId { get; set; }
        public string CategoryId { get; set; }
        public string Content { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public EquipmentCategory Category { get; set; }
    }
}
