using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Area;

[Table("RoomTypes")]
public class RoomType
{
    [Key]
    public string RoomTypeId { get; set; } // Khóa chính

    [Required]
    [StringLength(100)]
    public string TypeName { get; set; } // Tên loại phòng

    [StringLength(500)]
    public string? Description { get; set; } // Mô tả

    // Quan hệ 1-N với Room
    //public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}