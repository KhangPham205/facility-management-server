using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Area;

namespace backend.Models.Area
{
    [Table("Floors")]
    public class Floor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FloorId { get; set; }

        [Required]
        public string BuildingId { get; set; }

        [Required]
        [StringLength(100)]
        public string FloorName { get; set; }

        [Range(0,100)]
        public int RoomCount { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [ForeignKey("BuildingId")]
        public Building Building { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}