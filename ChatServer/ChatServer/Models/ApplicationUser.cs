using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public List<UserChat> UserChats { get; set; }
        public UserProfile UserProfile { get; set; }
        public bool Online { get; set; }
        public string ConnectionId { get; set; }
        public ApplicationUser()
        {
            UserChats = new List<UserChat>();
        }
    }
}
