namespace backend.DTOs.Import.Response
{
    public class ImportVoucherResponse
    {
        public string ImportId { get; set; }
        public string RequestId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        private string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public List<ImportVoucherDetailResponse> Details { get; set; }
    }
}
