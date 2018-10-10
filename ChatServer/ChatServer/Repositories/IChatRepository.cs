﻿using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IChatRepository:IRepositoryBase<Chat>
    {
        Task<IEnumerable<ChatMessage>> GetChatMessages(int chatId);
        Task<IEnumerable<Chat>> GetChatList();
        void SaveMessages(int chatId, ChatMessage chatMessage);
        bool IsUserInChat(string userName, int chatId);
        Task<IEnumerable<ApplicationUser>> GetChatUsers(int chatId);
    }
}
