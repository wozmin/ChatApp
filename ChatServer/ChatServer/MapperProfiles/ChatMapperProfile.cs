using AutoMapper;
using ChatServer.Models;
using ChatServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.MapperProfiles
{
    public class ChatMapperProfile:Profile
    {
        public ChatMapperProfile()
        {
            CreateMap<ChatMessage, ChatMessageModel>();
            CreateMap<Chat, ChatViewModel>();
        }
    }
}
