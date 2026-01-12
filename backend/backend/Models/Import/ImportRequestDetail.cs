using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    public class ImportRequestDetail
    {
        [Key]
        public string DetailId { get; set; }
        public string RequestId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("RequestId")]
        public ImportRequest Request { get; set; }
    }
}
