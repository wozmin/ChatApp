using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string UserName{get;set;}
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public virtual Chat Chat { get; set; }
        public int ChatId { get; set; }
    }
}
