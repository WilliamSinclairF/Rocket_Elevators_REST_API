using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Columns
    {
        public long Id { get; set; }
        public long? BatteryId { get; set; }
        public string ColumnType { get; set; }
        public string ColumnStatus { get; set; }
        public int? NumberOfFloorsServed { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ColumnDTO
    {
        public long Id { get; set; }
        public string ColumnStatus { get; set; }
    }
}
