using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Criteria.Request
{
    public class CreateCriteriaListRequest
    {
        [Required]
        public string? CategoryId { get; set; }

        [Required]
        public List<CriteriaContent> Contents { get; set; } = new();

        public class CriteriaContent
        {
            [Required]
            [StringLength(500)]
            public string Content { get; set; }
        }
    }
}
