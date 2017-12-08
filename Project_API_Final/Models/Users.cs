using System;
using System.Collections.Generic;

namespace Project_API_Final.Models
{
    public partial class Users
    {
        public Users()
        {
            Auth = new HashSet<Auth>();
            Posts = new HashSet<Posts>();
            Threads = new HashSet<Threads>();
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<Auth> Auth { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<Threads> Threads { get; set; }
    }
}
