using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Criteria.Request
{
    public class CreateCriteriaRequest
    {
        [Required]
        public string? CategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string? Content { get; set; }
    }
}
