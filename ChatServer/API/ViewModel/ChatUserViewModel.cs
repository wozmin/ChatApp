using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastVisit { get; set; }
        public string AvatarUrl { get; set; }
    }
}
