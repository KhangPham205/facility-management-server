namespace backend.DTOs.Import.Request
{
    public class CreateImportRequestRequest
    {
        public string CreatedBy { get; set; }
        public string? Reason { get; set; }
        public List<ImportRequestDetailDto> Details { get; set; } = new List<ImportRequestDetailDto>();
    }
}
