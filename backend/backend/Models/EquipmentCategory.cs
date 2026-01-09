using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class EquipmentCategory
    {
        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } // QĐ: Tên duy nhất

        public string? Description { get; set; }

        // Quan hệ 1-Nhiều
        public virtual ICollection<Equipment>? Equipments { get; set; }
    }
}
