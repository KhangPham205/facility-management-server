using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class FundSource
    {
        [Key]
        public string SourceId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string SourceName { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
