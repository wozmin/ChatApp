using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ChatMessage
    {
        public ApplicationUser User{get;set;}
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Chat Chat { get; set; }
    }
}
