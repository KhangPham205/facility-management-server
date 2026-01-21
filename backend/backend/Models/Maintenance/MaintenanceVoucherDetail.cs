using backend.Models.EquipmentInfo;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    [Table("MaintenanceVoucherDetails")]
    [EntityFilter(Prefix = "")]
    public class MaintenanceVoucherDetail
    {
        public string VoucherId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey(nameof(VoucherId))]
        public MaintenanceVoucher? MaintenanceVoucher { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment? Equipment { get; set; }
    }
}
