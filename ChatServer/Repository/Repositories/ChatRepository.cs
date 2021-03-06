﻿using ChatServer.EF;
using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        private ApplicationContext _db;

        public ChatRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<Chat> GetByIdAsync(Guid id)
        {
            return await _db.Chats
                .Include(c => c.UserChats)
                .Include(c => c.Messages)
                .ThenInclude(m => m.User)
                .ThenInclude(m => m.UserProfile)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<UserChat>> GetChatsByUser(string userId)
        {
            return await _db.UserChats
                .Include(uc => uc.Chat)
                .ThenInclude(c => c.Messages)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.UserProfile)
                .Where(uc => uc.ApplicationUserId == userId)
                .ToListAsync();

        }

        public async Task<Chat> GetByNameAsync(string name)
        {
            return await _db.Chats
                .Include(c => c.UserChats)
                .Include(c => c.Messages)
                .ThenInclude(m => m.User)
                .ThenInclude(m => m.UserProfile)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async override Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await _db.Chats
                .Include(c => c.Messages)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.UserProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(Guid chatId, int page, int pageSize)
        {
            return await _db.ChatMessages
                .Include(cm => cm.User)
                .ThenInclude(u => u.UserProfile)
                .OrderByDescending(m => m.Id)
                .Where(c => c.Chat.Id == chatId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void SaveMessages(Guid chatId, ChatMessage chatMessage)
        {
            var chat = _db.Chats.FirstOrDefault(c => c.Id == chatId);
            chat?.Messages.Add(chatMessage);
        }

        public bool IsUserInChat(string userId, Guid chatId)
        {
            return _db.UserChats.Any(u => u.ApplicationUserId == userId && u.ChatId == chatId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetChatUsersAsync(Guid chatId)
        {
            return await _db.UserChats
                 .Where(uc => uc.ChatId == chatId)
                 .Select(uc => uc.ApplicationUser)
                 .ToListAsync();
        }

        public void AddToChat(Guid chatId, string userId)
        {
            _db.UserChats.Add(new UserChat
            {
                ApplicationUserId = userId,
                ChatId = chatId
            });
        }
    }
}
