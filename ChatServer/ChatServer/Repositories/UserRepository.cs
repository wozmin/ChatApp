using ChatServer.EF;
using ChatServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class UserRepository:RepositoryBase<ApplicationUser>,IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db,UserManager<ApplicationUser> userManager):base(db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetActiveUsersAsync()
        {
           return await _db.AppUsers.Where(u => u.IsOnline).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _db.AppUsers.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public bool IsUserConnected(string connectionId)
        {
            return _db.AppUsers.Any(u => u.ConnectionId == connectionId);
        }
    }
}
