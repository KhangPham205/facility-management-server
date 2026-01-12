using backend.Models.EquipmentInfo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Liquidate
{
    public class LiquidateRequestDetail
    {
        // Composite Key
        public string RequestId { get; set; }
        public string EquipmentId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("RequestId")]
        public LiquidateRequest Request { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
