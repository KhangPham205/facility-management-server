
using System;

namespace DTOs.ExternalUnit.Response
{
    public class ExternalUnitResponse
    {
        public string UnitId { get; set; } = default!;

        public string UnitName { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string? TaxCode { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? BankName { get; set; }

        public string? Fax { get; set; }

        public DateTime? FromContractPeriod { get; set; }

        public DateTime? ToContractPeriod { get; set; }

        public string? FieldOfActivity { get; set; }

        public string? Supply { get; set; }
    }
}