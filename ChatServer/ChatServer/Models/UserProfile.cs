using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime LastVisit { get; set; }
        public string Avatar { get; set; }

        public ApplicationUser User { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
