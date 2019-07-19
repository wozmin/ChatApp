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

        public virtual ApplicationUser Creator { get; set; }
        public string CreatorId { get; set; }

        public virtual List<UserChat> UserChats { get; set; }
        public virtual List<ChatMessage> Messages { get; set; }
    }
}
