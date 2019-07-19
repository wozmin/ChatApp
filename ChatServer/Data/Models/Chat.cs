using System;
using System.Collections.Generic;

namespace ChatServer.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public ApplicationUser Creator { get; set; }
        public string CreatorId { get; set; }

        public List<UserChat> UserChats { get; set; }
        public List<ChatMessage> Messages { get; set; }
    }
}
