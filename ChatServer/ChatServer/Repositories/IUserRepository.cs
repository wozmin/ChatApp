using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    interface IUserRepository:IRepositoryBase<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IEnumerable<ApplicationUser>> GetActiveUsers();
    }
}
