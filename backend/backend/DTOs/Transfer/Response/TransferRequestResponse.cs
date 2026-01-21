using backend.Enums;

namespace backend.DTOs.Transfer.Response
{
    public class TransferRequestResponse
    {
        public string RequestId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string SourceLocationId { get; set; }
        public string SourceLocationName { get; set; }
        public LocationType SourceLocationType { get; set; }
        public string DestinationLocationId { get; set; }
        public string DestinationLocationName { get; set; }
        public LocationType DestinationLocationType { get; set; }
        public string Note { get; set; }
        public VoucherStatus Status { get; set; }
        public List<TransferRequestDetailResponse> Details { get; set; }
    }
}
