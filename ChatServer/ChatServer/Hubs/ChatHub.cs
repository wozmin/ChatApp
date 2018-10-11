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
            await this.ChangeUserStatusAsync(true);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await this.ChangeUserStatusAsync(false);
            await base.OnDisconnectedAsync(exception);
        }


        public async Task AddUserToChat(ApplicationUser user,string chatName)
        {
            await Groups.AddToGroupAsync(user.ConnectionId, chatName);
        }

        public  async Task SendMessage(ChatMessageViewModel chatMessage)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(chatMessage.ChatId);
            var user = await _unitOfWork.Users.GetUserByNameAsync(chatMessage.UserName);
            if(chat== null || user == null)
            {
                return;
            }
            if (!_unitOfWork.Chats.IsUserInChat(user.UserName,chat.Id)) {
                return;
            }
            await AddUserToChat(user, chat.Name);
            await Clients.Group(chat.Name).SendAsync("SendMessage",chatMessage);
            chat.Messages.Add(new ChatMessage { Chat = chat, Message = chatMessage.Message, UserName = user.UserName, Date = DateTime.Now });
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ChangeUserStatusAsync(bool online)
        {
            var userName = Context.User.Identity.Name;
            var user = await _unitOfWork.Users.GetUserByNameAsync(userName);
            if (user != null)
            {
                if (online) {
                    user.ConnectionId = Context.ConnectionId;
                }
                user.IsOnline = online;
                await _unitOfWork.SaveChangesAsync();
            }

        }
    }
}
