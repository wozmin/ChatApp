using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatMessageViewModel
    {
        public string Message;
        public string UserName;
        public int ChatId { get; set; }
    }
}
