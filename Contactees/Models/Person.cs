using System;
using System.Collections.Generic;

#nullable disable

namespace Contactees.Server.Models
{
    public partial class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
            Contacts = new HashSet<Contact>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long IdNumber { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
