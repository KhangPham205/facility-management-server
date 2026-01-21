using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    [Table("LiquidateRequestDetails")]
    [EntityFilter(Prefix = "")]
    public class LiquidateRequestDetail
    {
        // Composite Key
        [Required]
        public string RequestId { get; set; }
        [Required]
        public string EquipmentId { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }


        [ForeignKey("RequestId")]
        public LiquidateRequest Request { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
