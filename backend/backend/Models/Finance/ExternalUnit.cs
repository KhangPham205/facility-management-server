using Plainquire.Filter.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Finance
{
    [Table("ExternalUnits")]
    [EntityFilter(Prefix = "")]
    public class ExternalUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public DateTime? FromContractPeriod { get; set; }
        public DateTime? ToContractPeriod { get; set; }
        public string? FieldOfActivity { get; set; }
        public string? Supply { get; set; }
    }
}
