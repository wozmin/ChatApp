using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class ChatDetailViewModel
    {
        public string Name { get; set; }
        public List<ChatUserViewModel> Members { get; set; }
    }
}
