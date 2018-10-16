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
            CreateMap<ChatMessage, ChatMessageModel>()
                .ForMember(c=>c.MessageDate, opt=>opt.MapFrom(cmm=>cmm.Date))
                .ForMember(c=>c.MessageText,opt=>opt.MapFrom(cmm=>cmm.Message));
            CreateMap<Chat, ChatViewModel>()
                .ForMember(c=>c.LastMessageUserName,opt=>opt.MapFrom(cvm=>cvm.Messages.Last().UserName))
                .ForMember(c=>c.LastMessageText, opt=>opt.MapFrom(cvm=>cvm.Messages.Last().Message))
                .ForMember(c=>c.LastMessageDate,opt=>opt.MapFrom(cvm=>cvm.Messages.Last().Date));
        }
    }
}
