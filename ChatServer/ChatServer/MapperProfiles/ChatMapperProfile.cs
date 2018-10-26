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
                .ForMember(c=>c.UserName,opt=>opt.MapFrom(cmm=>cmm.User.UserName))
                .ForMember(c=>c.MessageDate, opt=>opt.MapFrom(cmm=>cmm.Date))
                .ForMember(c=>c.MessageText,opt=>opt.MapFrom(cmm=>cmm.Message))
                .ForMember(c=>c.UserAvatarUrl,opt=>opt.MapFrom(cmm=>cmm.User.UserProfile.AvatarUrl));
            CreateMap<UserChat, ChatViewModel>()
                .ForMember(c=>c.Id,opt=>opt.MapFrom(cvm=>cvm.ChatId))
                .ForMember(c=>c.Name,opt=>opt.MapFrom(cvm=>cvm.Chat.Name))
                .ForMember(c => c.LastMessageUserName, opt => opt.MapFrom(cvm => cvm.Chat.Messages.LastOrDefault().User.UserName))
                .ForMember(c => c.LastMessageText, opt => opt.MapFrom(cvm => cvm.Chat.Messages.LastOrDefault().Message))
                .ForMember(c => c.LastMessageDate, opt => opt.MapFrom(cvm => cvm.Chat.Messages.LastOrDefault().Date));
        }
    }
}
