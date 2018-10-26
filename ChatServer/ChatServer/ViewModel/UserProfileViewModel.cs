using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public DateTime LastVisit { get; set; }
        public bool IsOnline { get; set; }
        public string AvatarUrl { get; set; }
    }
}
