using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Leads
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string ProjectName { get; set; }
        public string Department { get; set; }
        public string ProjectDescription { get; set; }
        public string Message { get; set; }
        public string Attachment { get; set; }
        public long? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
