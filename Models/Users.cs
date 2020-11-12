using System;
using System.Collections.Generic;

namespace Rocket_REST_API.Models
{
    public partial class Users
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordSentAt { get; set; }
        public DateTime? RememberCreatedAt { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsEmployee { get; set; }
        public bool? IsUser { get; set; }
        public string Phone { get; set; }
    }
}
