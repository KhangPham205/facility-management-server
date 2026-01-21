using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace backend.Models.Area
{
    [Table("Buildings")]
    [EntityFilter(Prefix = "")]
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BuildingId { get; set; }

        [Required]
        [StringLength(100)]
        public string BuildingName { get; set; }

        [Required]
        [Range(0, 100)]
        public int FloorCount { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        public ICollection<Floor> Floors { get; set; } = new List<Floor>();
    }
}