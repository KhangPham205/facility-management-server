using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Area;

[Table("Rooms")]
public class Room
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string RoomId { get; set; }

    [Required]
    public string FloorId { get; set; }

    [Required]
    public string RoomName { get; set; }

    [Required]
    public string RoomTypeId { get; set; }

    public int Capacity { get; set; }
    public string Status { get; set; }
    public string? Note { get; set; }

    [ForeignKey("FloorId")]
    public Floor Floor { get; set; }

    [ForeignKey("RoomTypeId")]
    public RoomType RoomType { get; set; }
}