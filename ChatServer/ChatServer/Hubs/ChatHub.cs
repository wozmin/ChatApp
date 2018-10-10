using ChatServer.Models;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                await _unitOfWork.SaveChangesAsync();
            }
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User.Identity.Name;
            var user = await _unitOfWork.Users.GetUserByName(userName);
            if (user != null)
            {
                user.Online = false;
                await _unitOfWork.SaveChangesAsync();
            }
            await base.OnDisconnectedAsync(exception);
        }


        public async Task AddUserToChat(ApplicationUser user,string chatName)
        {
            await Groups.AddToGroupAsync(user.ConnectionId, chatName);
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
            await AddUserToChat(user, chat.Name);
            await Clients.Group(chat.Name).SendAsync("SendMessage",chatMessage);
            chat.Messages.Add(new ChatMessage { Chat = chat, Message = chatMessage.Message, User = user.UserName, Date = DateTime.Now });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
