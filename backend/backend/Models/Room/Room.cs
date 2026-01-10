using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Rooms")]
public class Room
{
    [Key]
    public string RoomId { get; set; } //

    [Required]
    public string FloorId { get; set; } //

    [Required]
    public string RoomName { get; set; } //

    [Required]
    public string RoomTypeId { get; set; } //

    public int Capacity { get; set; } //

    [Required]
    public string Status { get; set; } //

    public string? Note { get; set; } //

    // Navigation Properties
    [ForeignKey("FloorId")]
    public virtual Floor Floor { get; set; }

    [ForeignKey("RoomTypeId")]
    public virtual RoomType RoomType { get; set; }
}