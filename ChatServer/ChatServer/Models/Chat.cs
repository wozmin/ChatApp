using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public UserProfile Creator { get; set; }
        public List<UserProfile> Members { get; set; }
        public Chat()
        {
            Members = new List<UserProfile>();
        }
    }
}
