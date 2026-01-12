using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Transfer
{
    [Table("TransferRequests")]
    public class TransferRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestId { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Vị trí nguồn (Linh động: Có thể là Room hoặc Kho)
        public string SourceLocationId { get; set; }
        public LocationType SourceLocationType { get; set; }

        // Vị trí đích (Thường là Room cụ thể)
        public string DestinationRoomId { get; set; }
        public LocationType DestinationLocationType { get; set; }

        public string? Reason { get; set; }
        public VoucherStatus Status { get; set; }

        public string? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public User? Approver { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public ICollection<TransferRequestDetail> Details { get; set; }
    }
}
