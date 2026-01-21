using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Finance
{
    [Table("FundSources")]
    [EntityFilter(Prefix = "")]
    public class FundSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SourceId { get; set; }
        public string SourceName { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
