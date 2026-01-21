using backend.Models.EquipmentInfo;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Borrow
{
    [Table("BorrowVoucherDetails")]
    [EntityFilter(Prefix = "")]
    public class BorrowVoucherDetail
    {
        // Composite Key: Cấu hình trong DbContext
        public string BorrowId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey("BorrowId")]
        public BorrowVoucher BorrowVoucher { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
