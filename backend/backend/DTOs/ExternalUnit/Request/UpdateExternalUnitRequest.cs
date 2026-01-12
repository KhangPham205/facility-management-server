
using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs.ExternalUnit.Request
{
    public class UpdateExternalUnitRequest
    {
        [Required, MaxLength(200)]
        public string UnitName { get; set; } = default!;

        [Required, MaxLength(300)]
        public string Address { get; set; } = default!;

        [Required, Phone, MaxLength(30)]
        public string PhoneNumber { get; set; } = default!;

        [MaxLength(50)]
        public string? TaxCode { get; set; }

        [MaxLength(50)]
        public string? BankAccountNumber { get; set; }

        [MaxLength(150)]
        public string? BankName { get; set; }

        [MaxLength(30)]
        public string? Fax { get; set; }

        public DateTime? FromContractPeriod { get; set; }

        public DateTime? ToContractPeriod { get; set; }

        [MaxLength(150)]
        public string? FieldOfActivity { get; set; }

        [MaxLength(300)]
        public string? Supply { get; set; }
    }
}
