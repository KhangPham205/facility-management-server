using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("AuditDetails")]
    [PrimaryKey(nameof(AuditId), nameof(EquipmentId))] // Định nghĩa khóa chính tổ hợp
    public class AuditDetail
    {
        [Key]
        public string DetailId { get; set; } // Bảng này nên có ID riêng để dễ quản lý sai lệch

        public string AuditId { get; set; }
        [ForeignKey("AuditId")]
        public InventoryAudit InventoryAudit { get; set; }

        public string EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }

        public int BookQuantity { get; set; }   // Số lượng trên hệ thống
        public int ActualQuantity { get; set; } // Số lượng đếm được

        // Difference = Actual - Book (Hệ thống tự tính hoặc lưu cứng)
        public int Difference { get; set; }

        public string? Condition { get; set; } // Tình trạng thực tế (Tốt/Hỏng)
        public string? Note { get; set; }
    }
}