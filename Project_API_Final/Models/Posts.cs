using System;
using System.Collections.Generic;

namespace Project_API_Final.Models
{
    public partial class Posts
    {
        public Guid PostId { get; set; }
        public Guid ThreadId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime PostedOn { get; set; }

        public Threads Thread { get; set; }
        public Users User { get; set; }
    }
}
