using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Borrow
{
    public class BorrowVoucherDetail
    {
        // Composite Key: Cấu hình trong DbContext
        public string BorrowId { get; set; }
        public string EquipmentId { get; set; }

        public int Quantity { get; set; }
        public string? ConditionBefore { get; set; } // Tình trạng lúc mượn
        public string? ConditionAfter { get; set; }  // Tình trạng lúc trả

        [ForeignKey("BorrowId")]
        public BorrowVoucher BorrowVoucher { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
