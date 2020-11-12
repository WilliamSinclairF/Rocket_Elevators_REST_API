using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Addresses
    {
        public long Id { get; set; }
        public string TypeOfAddress { get; set; }
        public string Status { get; set; }
        public string Entity { get; set; }
        public string NumberAndStreet { get; set; }
        public string SuiteOrApartment { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public long? BuildingId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Buildings Building { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
