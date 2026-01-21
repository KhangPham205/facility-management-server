using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Area;

[Table("Rooms")]
[EntityFilter(Prefix = "")]
public class Room
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string RoomId { get; set; }

    [Required]
    public string FloorId { get; set; }

    [Required]
    [StringLength(100)]
    public string RoomName { get; set; }

    [Required]
    public string RoomTypeId { get; set; }

    [Range(0, int.MaxValue)]
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    [ForeignKey("FloorId")]
    public Floor Floor { get; set; }

    [ForeignKey("RoomTypeId")]
    public RoomType RoomType { get; set; }
}