using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    [Table("ImportRequests")]
    public class ImportRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
        public VoucherStatus Status { get; set; }
        public string? ApprovedBy { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public User Approver { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public ICollection<ImportRequestDetail> Details { get; set; }
    }
}
