using backend.Enums;
using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Finance
{
    [Table("Invoices")]
    [EntityFilter(Prefix = "")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public FunctionType Type { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Note { get; set; }

        [ForeignKey(nameof(UnitId))]
        public ExternalUnit Unit { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User Creator { get; set; }
    }
}