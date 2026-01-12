using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.EquipmentInfo
{
    public class Criteria
    {
        [Key]
        public string CriteriaId { get; set; }
        public string CategoryId { get; set; }
        public string Content { get; set; }

        [ForeignKey("CategoryId")]
        public EquipmentCategory Category { get; set; }
    }
}
