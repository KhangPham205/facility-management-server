using System.ComponentModel.DataAnnotations;

namespace backend.Models.Finance
{
    public class FundSource
    {
        [Key]
        public string SourceId { get; set; }
        public string SourceName { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
