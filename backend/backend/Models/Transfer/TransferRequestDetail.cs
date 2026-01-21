using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Transfer
{
    [Table("TransferRequestDetails")]
    [EntityFilter(Prefix = "")]
    public class TransferRequestDetail
    {
        // Composite Key
        public string RequestId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey("RequestId")]
        public TransferRequest Request { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
