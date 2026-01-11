using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace backend.Models.Area
{
    [Table("Buildings")] // Chỉ định tên bảng nếu cần
    public class Building
    {
        [Key]
        public string BuildingId { get; set; }

        [Required]
        [StringLength(100)]
        public string BuildingName { get; set; }

        [Required]
        public int FloorCount { get; set; }

        public string? Note { get; set; }

        // Relationship: 1 Building - Many Floors
        public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();
    }
}