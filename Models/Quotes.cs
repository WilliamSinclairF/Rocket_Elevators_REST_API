using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Quotes
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public int? Apartments { get; set; }
        public int? Floors { get; set; }
        public int? Basements { get; set; }
        public int? Businesses { get; set; }
        public int? ElevatorShafts { get; set; }
        public int? ParkingSpaces { get; set; }
        public int? Occupants { get; set; }
        public int? OpeningHours { get; set; }
        public string ProductLine { get; set; }
        public decimal? InstallFee { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? UnitPrice { get; set; }
        public int? ElevatorNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string BuildingType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
