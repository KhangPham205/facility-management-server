using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Area;

[Table("RoomTypes")]
public class RoomType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string RoomTypeId { get; set; }

    [Required]
    [StringLength(100)]
    public string TypeName { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}