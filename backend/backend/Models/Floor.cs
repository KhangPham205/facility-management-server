using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.Models
{
    [Table("Floors")]
    public class Floor
    {
        [Key]
        public string FloorId { get; set; }

        [Required]
        public string BuildingId { get; set; }

        [Required]
        public string FloorName { get; set; }

        public string? Note { get; set; }

        // Mối quan hệ N-1 với Building
        [ForeignKey("BuildingId")]
        public virtual Building Building { get; set; }

        // Mối quan hệ 1-N với Room (theo sơ đồ)
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}