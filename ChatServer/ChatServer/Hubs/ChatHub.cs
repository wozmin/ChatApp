using AutoMapper;
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
        private readonly IMapper _mapper; 
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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


        public async Task JoinChat(string userId,int chatId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            await Groups.AddToGroupAsync(user.ConnectionId, chatId.ToString());
            if (!_unitOfWork.Chats.IsUserInChat(user.UserName, chatId))
            {
                var chat = await _unitOfWork.Chats.GetByIdAsync(chatId);
                await Clients.Client(user.ConnectionId).SendAsync("JoinChat", _mapper.Map<ChatViewModel>(chat));
            }
        }

        public  async Task SendMessage(ChatMessageViewModel chatMessage)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(chatMessage.ChatId);
            var user = await _unitOfWork.Users.GetUserByNameAsync(chatMessage.UserName);
            var currentUser = await _unitOfWork.Users.GetUserByNameAsync(Context.User.Identity.Name);
            if(chat== null || user == null)
            {
                return;
            }
            if (!_unitOfWork.Chats.IsUserInChat(user.UserName,chat.Id)) {
                return;
            }
            //await AddUserToChat(currentUser, chat.Name);
            //await AddUserToChat(user, chat.Name);
            var message = new ChatMessageModel { MessageText = chatMessage.Message, MessageDate = DateTime.Now, UserAvatarUrl = user.UserProfile?.AvatarUrl, UserName = chatMessage.UserName };
            await Clients.Group(chat.Id.ToString()).SendAsync("SendMessage",message);
            await Clients.Caller.SendAsync("SendMessage",message);
            chat.Messages.Add(new ChatMessage { Chat = chat, Message = chatMessage.Message, User = user, Date = DateTime.Now });
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
                else
                {
                    user.LastVisit = DateTime.Now;
                }
                user.IsOnline = online;
                await _unitOfWork.SaveChangesAsync();
            }

        }
    }
}
