using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatMessageModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
