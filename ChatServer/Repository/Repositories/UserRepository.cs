using ChatServer.EF;
using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ApplicationUser>> GetActiveUsersAsync()
        {
            return await _db.AppUsers.Where(u => u.IsOnline).ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(int page, int pageSize)
        {
            return await _db.AppUsers
                .Include(u => u.UserProfile)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            return await _db.AppUsers.Include(u => u.UserProfile).FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public bool IsUserConnected(string connectionId)
        {
            return _db.AppUsers.Any(u => u.ConnectionId == connectionId);
        }

        public async Task<UserProfile> GetUserProfile(string userId)
        {
            return await _db.UserProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.User.Id == userId);
        }
    }
}
