using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Criteria.Request
{
    public class UpdateCriteriaRequest
    {
        public string? CategoryId { get; set; }

        [StringLength(500)]
        public string? Content { get; set; }
    }
}
