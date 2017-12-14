using System;
using System.Collections.Generic;

namespace Project_API_Final.Models.Auto
{
    public partial class Auth
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
