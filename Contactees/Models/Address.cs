using System;
using System.Collections.Generic;

#nullable disable

namespace Contactees.Server.Models
{
    public partial class Address
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }
        public string Country { get; set; }

        public virtual Person Person { get; set; }
    }
}
