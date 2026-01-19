using backend.Enums;

namespace backend.DTOs.Transfer.Response
{
    public class TransferVoucherResponse
    {
        public string TransferId { get; set; }
        public string RequestId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        public string? CreatedBy { get; set; }

        // Source
        public string SourceLocationId { get; set; }
        public LocationType SourceLocationType { get; set; }
        public string SourceLocationName { get; set; }

        // Destination
        public string DestinationLocationId { get; set; }
        public LocationType DestinationLocationType { get; set; }
        public string DestinationLocationName { get; set; }

        public List<TransferVoucherDetailResponse> Details { get; set; }
    }
}