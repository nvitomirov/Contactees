using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Contactees.Web.Models
{
    public partial class Address
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(7)]
        public string StreetNumber { get; set; }

        [StringLength(100)]
        public string City { get; set; }
        public int? PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public virtual Person Person { get; set; }
    }
}
