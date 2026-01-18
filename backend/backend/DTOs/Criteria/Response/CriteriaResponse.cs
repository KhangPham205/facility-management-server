using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs.Criteria.Response
{
    public class CriteriaResponse
    {
        public string? CriteriaId { get; set; }
        public string? CategoryId { get; set; }
        public string? Content { get; set; }
    }
}
