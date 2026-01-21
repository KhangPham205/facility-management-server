using backend.Models.EquipmentInfo;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Import
{
    [Table("ImportVoucherDetails")]
    [EntityFilter(Prefix = "")]
    public class ImportVoucherDetail
    {
        [Required]
        public string ImportId { get; set; }

        [Required]
        public string EquipmentId { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [ForeignKey("ImportId")]
        public ImportVoucher ImportVoucher { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
