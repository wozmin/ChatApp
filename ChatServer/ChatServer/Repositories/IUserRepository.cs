using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IUserRepository:IRepositoryBase<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IEnumerable<ApplicationUser>> GetActiveUsersAsync();
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<UserProfile> GetUserProfile(string userId);
        bool IsUserConnected(string connectionId);
    }
}
