using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatServer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public UserProfile UserProfile { get; set; }
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; }
        public DateTime LastVisit { get; set; }

        public virtual List<UserChat> UserChats { get; set; }
        public virtual List<ChatMessage> ChatMessages { get; set; }
    }
}
