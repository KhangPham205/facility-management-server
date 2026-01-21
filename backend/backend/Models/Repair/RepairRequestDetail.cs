using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Repair
{
    [Table("RepairRequestDetails")]
    [EntityFilter(Prefix = "")]
    public class RepairRequestDetail
    {
        // Composite Key
        public string RequestId { get; set; }
        public string EquipmentId { get; set; }

        public string? Note { get; set; }
        public string? Image { get; set; }

        [ForeignKey("RequestId")]
        public RepairRequest Request { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
