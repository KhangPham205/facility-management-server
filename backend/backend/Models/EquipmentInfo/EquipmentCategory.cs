using System.ComponentModel.DataAnnotations;

namespace backend.Models.EquipmentInfo
{
    public class EquipmentCategory
    {
        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } // QĐ: Tên duy nhất

        public string? Description { get; set; }

        public virtual ICollection<Criteria>? Criterias { get; set; }
    }
}
