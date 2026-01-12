using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    public class MaintenanceRequestDetail
    {
        // Composite Key
        public string RequestId { get; set; }
        public string EquipmentId { get; set; }

        public string? Description { get; set; } // Mô tả hư hỏng
        public string? Image { get; set; }

        [ForeignKey("RequestId")]
        public MaintenanceRequest Request { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
