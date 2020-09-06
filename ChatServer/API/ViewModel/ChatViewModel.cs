using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastMessageUserName { get; set; }
        public  DateTime LastMessageDate { get; set; }
        public string LastMessageText { get; set; }
    }
}
