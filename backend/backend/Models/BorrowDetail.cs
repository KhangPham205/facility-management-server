using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class BorrowDetail
    {
        public string BorrowId { get; set; }

        [ForeignKey("BorrowId")]
        public virtual BorrowVoucher? BorrowVoucher { get; set; }

        public string EquipmentId { get; set; }
        // public virtual Equipment Equipment { get; set; }

        public string EquipmentName { get; set; }

        public int Quantity { get; set; }
    }
}