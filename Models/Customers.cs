using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Addresses = new HashSet<Addresses>();
        }

        public long Id { get; set; }
        public long? UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactFullName { get; set; }
        public string CompanyContactPhone { get; set; }
        public string CompanyContactEmail { get; set; }
        public string CompanyDescription { get; set; }
        public string TechnicalAuthorityFullName { get; set; }
        public string TechnicalAuthorityPhoneNumber { get; set; }
        public string TechnicalManagerEmailService { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? AddressId { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
    }
}
