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

        [Required]
        public string RequestId { get; set; }

        [Required]
        public string EquipmentName { get; set; }

        public int Quantity { get; set; } = 1;

        [StringLength(500)]
        public string? Note { get; set; }

        [ForeignKey("RequestId")]
        public ImportRequest Request { get; set; }
    }
}
