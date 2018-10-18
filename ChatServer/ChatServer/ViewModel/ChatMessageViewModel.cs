using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatMessageViewModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public int ChatId { get; set; }
    }
}
