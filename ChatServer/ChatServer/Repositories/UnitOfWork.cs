using ChatServer.EF;
using ChatServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepo;
        private IChatRepository _chatRepo;
        private ApplicationContext _db { get; set; }
        private UserManager<ApplicationUser> _userManager;


        public UnitOfWork(ApplicationContext db,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IUserRepository Users {
            get
            {
                if(_userRepo == null)
                {
                    _userRepo = new UserRepository(_db,_userManager);
                }
                return _userRepo;
            }
        }

        public IChatRepository Chats {
            get
            {
                if(_chatRepo == null)
                {
                    _chatRepo = new ChatRepository(_db);
                }
                return _chatRepo;
            }
        }

        public async  Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
