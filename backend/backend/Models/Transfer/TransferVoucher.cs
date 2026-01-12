using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Transfer
{
    [Table("TransferVouchers")]
    public class TransferVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TransferId { get; set; }

        public string RequestId { get; set; }
        [ForeignKey("RequestId")]
        public TransferRequest Request { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Lưu snapshot vị trí tại thời điểm chuyển để đối chiếu
        public string SourceLocationId { get; set; }
        public string DestinationRoomId { get; set; }

        public ICollection<TransferVoucherDetail> Details { get; set; }
    }
}
