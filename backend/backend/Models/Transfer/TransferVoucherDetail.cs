using backend.Models.EquipmentInfo;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Transfer
{
    [Table("TransferVoucherDetails")]
    [EntityFilter(Prefix = "")]
    public class TransferVoucherDetail
    {
        // Composite Key
        public string TransferId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey("TransferId")]
        public TransferVoucher TransferVoucher { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
