using backend.Models.EquipmentInfo;
using backend.Models.Maintenance;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Repair
{
    [Table("RepairVoucherDetails")]
    [EntityFilter(Prefix = "")]
    public class RepairVoucherDetail
    {
        public string RepairId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey(nameof(RepairId))]
        public RepairVoucher? RepairVoucher { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment? Equipment { get; set; }
    }
}
