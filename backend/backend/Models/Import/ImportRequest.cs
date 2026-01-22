using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    [Table("ImportRequests")]
    [EntityFilter(Prefix = "")]
    public class ImportRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RequestId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Note { get; set; }
        public VoucherStatus Status { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User Creator { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public User Approver { get; set; }

        public ICollection<ImportRequestDetail> Details { get; set; } = new List<ImportRequestDetail>();
    }
}
