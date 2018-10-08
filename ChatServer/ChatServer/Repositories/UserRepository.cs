using ChatServer.EF;
using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class UserRepository:RepositoryBase<ApplicationUser>,IUserRepository
    {
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db):base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ApplicationUser>> GetActiveUsers()
        {
           return await _db.AppUsers.Where(u => u.Online).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _db.AppUsers.ToListAsync();
        }


    }
}
