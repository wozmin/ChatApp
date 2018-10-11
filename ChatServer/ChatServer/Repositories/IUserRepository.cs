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
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        bool IsUserConnected(string connectionId);
    }
}
