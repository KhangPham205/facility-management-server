using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.EquipmentInfo
{
    [Table("Criterias")]
    [EntityFilter(Prefix = "")]
    public class Criteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? CriteriaId { get; set; }

        [Required]
        public string? CategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string? Content { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public EquipmentCategory? Category { get; set; }
    }
}
