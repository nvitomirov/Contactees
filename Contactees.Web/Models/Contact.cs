using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Contactees.Web.Models
{
    public partial class Contact
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public virtual Person Person { get; set; }
    }
}
