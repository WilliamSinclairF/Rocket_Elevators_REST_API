using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Elevators
    {
        public long Id { get; set; }
        public long? ColumnId { get; set; }
        public string SerialNumber { get; set; }
        public string ElevatorModel { get; set; }
        public string ElevatorType { get; set; }
        public string ElevatorStatus { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public DateTime? DateOfLastInspection { get; set; }
        public string CertificateOfInspection { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ElevatorDTO
    {
        public long Id { get; set; }
        public string ElevatorStatus { get; set; }
    }
}
