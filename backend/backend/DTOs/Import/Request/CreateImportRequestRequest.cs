using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Import.Request
{
    public class CreateImportRequestRequest
    {
        [StringLength(500)]
        public string? Note { get; set; }

        public List<ImportRequestDetailDto> Details { get; set; } = new List<ImportRequestDetailDto>();
    }
}
