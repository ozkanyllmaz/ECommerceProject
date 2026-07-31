using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities.ValueObjects
{
    public class Address
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string FullAddress { get; set; } = null!;

        public string InvoiceType { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxOffice { get; set; }

    }
}
