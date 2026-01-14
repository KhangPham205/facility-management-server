using backend.Models.EquipmentInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    [Table("ImportVoucherDetails")]
    public class ImportVoucherDetail
    {
        public string ImportId { get; set; }
        public string EquipmentId { get; set; }
        public string? Note { get; set; }

        [ForeignKey("ImportId")]
        public ImportVoucher ImportVoucher { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
