using ChatServer.EF;
using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class ChatRepository:RepositoryBase<Chat>,IChatRepository
    {
        private ApplicationContext _db;

        public ChatRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<Chat> GetById(int id)
        {
            return await _db.Chats.Include(c => c.UserChats).Include(c=>c.Messages).FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<IEnumerable<Chat>> GetChatList()
        {
            return await _db.Chats.Include(c=>c.Creator).ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(int chatId)
        {
            return await _db.ChatMessages.Where(c => c.Chat.Id == chatId).ToListAsync();
        }

        public void SaveMessages(int chatId,ChatMessage chatMessage)
        {
            var chat = _db.Chats.FirstOrDefault(c => c.Id == chatId);
            if(chat == null)
            {
                return;
            }
            chat.Messages.Add(chatMessage);
        }

        public bool IsUserInChat(string userName,int chatId)
        {
            var chat = _db.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                return false;
            }
            var user = _db.AppUsers.FirstOrDefault(u => u.UserName == userName);
            if(user == null)
            {
                return false;
            }
            return chat.UserChats.Any(u=>u.ApplicationUserId == user.Id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetChatUsers(int chatId)
        {
           return await _db.UserChats.Where(uc => uc.ChatId == chatId).Select(uc => uc.ApplicationUser).ToListAsync();
        }
    }
}
