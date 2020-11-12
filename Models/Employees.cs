using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Employees
    {
        public Employees()
        {
            BuildingsAdminContact = new HashSet<Buildings>();
            BuildingsTechnicalContact = new HashSet<Buildings>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? UserId { get; set; }

        public virtual ICollection<Buildings> BuildingsAdminContact { get; set; }
        public virtual ICollection<Buildings> BuildingsTechnicalContact { get; set; }
    }
}
