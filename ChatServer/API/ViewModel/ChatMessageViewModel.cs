using System;

namespace ChatServer.ViewModel
{
    public class ChatMessageViewModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public Guid ChatId { get; set; }
    }
}
