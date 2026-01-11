using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ExternalUnit
    {
        [Key]
        //[Column("unit_id")]
        public string UnitId { get; set; } = null!;

        //[Column("unit_name")]
        [StringLength(255)]
        public string UnitName { get; set; } = null!;

        //[Column("address")]
        public string? Address { get; set; }

        //[Column("tax_code")]
        public string? TaxCode { get; set; }

        //[Column("from_contract_period")]
        public DateTime? FromContractPeriod { get; set; }

        //[Column("to_contract_period")]
        public DateTime? ToContractPeriod { get; set; }

        //[Column("supply")]
        public string? Supply { get; set; }

        // Navigation properties (Nếu bạn có bảng Voucher tham chiếu đến Supplier)
         public virtual ICollection<ImportVoucher> ImportVouchers { get; set; } = new List<ImportVoucher>();
    }
}
