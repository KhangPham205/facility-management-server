using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    [Table("ImportRequestDetails")]
    public class ImportRequestDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DetailId { get; set; }
        public string RequestId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }

        [ForeignKey("RequestId")]
        public ImportRequest Request { get; set; }
    }
}
