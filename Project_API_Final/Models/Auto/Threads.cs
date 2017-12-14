using System;
using System.Collections.Generic;

namespace Project_API_Final.Models.Auto
{
    public partial class Threads
    {
        public Threads()
        {
            Posts = new HashSet<Posts>();
        }
        public Guid ThreadId { get; set; }
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public DateTime PostedOn { get; set; }

        public Users User { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}
