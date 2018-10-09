using ChatServer.Models;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
   
    public class ChatHub:Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            var user = await _unitOfWork.Users.GetUserByName(userName);
            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                user.Online = true;
            }
            await base.OnConnectedAsync();
        }


        public async Task AddUserToChat(ApplicationUser user,string chatName)
        {
            await Groups.AddToGroupAsync(user.ConnectionId, chatName);
            _unitOfWork.Chats.Create(new Chat { Name = chatName, CreationDate = DateTime.Now, Creator = user });
            await _unitOfWork.SaveChangesAsync();
        }

        public  async Task SendMessage(ChatMessageViewModel chatMessage)
        {
            var chat = await _unitOfWork.Chats.GetById(chatMessage.ChatId);
            var user = await _unitOfWork.Users.GetUserByName(chatMessage.UserName);
            if(chat== null || user == null)
            {
                return;
            }
            if (!_unitOfWork.Chats.IsUserInChat(user.UserName,chat.Id)) {
                return;
            }
            await Clients.Group(chat.Name).SendAsync("SendMessage",chatMessage);
            chat.Messages.Add(new ChatMessage { Chat = chat, Message = chatMessage.Message, User = user.UserName, Date = DateTime.Now });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
