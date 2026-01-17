using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Borrow
{
    [Table("BorrowVoucherDetails")]
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
