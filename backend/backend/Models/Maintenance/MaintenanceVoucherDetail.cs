using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Maintenance
{
    [Table("MaintenanceVoucherDetails")]
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
