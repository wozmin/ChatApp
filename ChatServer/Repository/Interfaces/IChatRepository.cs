using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IChatRepository : IRepositoryBase<Chat>
    {
        Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(Guid chatId, int page, int pageSize);
        Task<IEnumerable<UserChat>> GetChatsByUser(string userId);
        void SaveMessages(Guid chatId, ChatMessage chatMessage);
        bool IsUserInChat(string userId, Guid chatId);
        Task<IEnumerable<ApplicationUser>> GetChatUsersAsync(Guid chatId);
        void AddToChat(Guid chatId, string userId);
    }
}
