using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Contactees.Web.Models
{
    public partial class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
            Contacts = new HashSet<Contact>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Surname { get; set; }

        [Required]
        public long IdNumber { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
