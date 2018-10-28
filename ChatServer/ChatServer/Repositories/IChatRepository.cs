using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IChatRepository:IRepositoryBase<Chat>
    {
        Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(int chatId,int page,int pageSize);
        Task<IEnumerable<UserChat>> GetChatsByUser(string userId);
        void SaveMessages(int chatId, ChatMessage chatMessage);
        bool IsUserInChat(string userId, int chatId);
        Task<IEnumerable<ApplicationUser>> GetChatUsersAsync(int chatId);
        void AddToChat(int chatId, string userId);
    }
}
