using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Chat Chat { get; set; }
        public Guid ChatId { get; set; }
    }
}
