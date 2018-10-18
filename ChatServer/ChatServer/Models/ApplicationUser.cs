using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public UserProfile UserProfile { get; set; }
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; }

        public virtual List<UserChat> UserChats { get; set; }
        public virtual List<ChatMessage> ChatMessages { get; set; }
    }
}
