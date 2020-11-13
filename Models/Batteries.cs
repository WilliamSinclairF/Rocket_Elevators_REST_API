using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Batteries
    {
        public long Id { get; set; }
        public long? BuildingId { get; set; }
        public long? EmployeeId { get; set; }
        public string BatteryType { get; set; }
        public string BatteryStatus { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public DateTime? DateOfLastInspection { get; set; }
        public string CertificateOfOperations { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class BatteryDTO
    {
        public long Id { get; set; }
        public string BatteryStatus { get; set; }
    }
}
