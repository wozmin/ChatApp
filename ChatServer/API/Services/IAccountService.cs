using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public interface IAccountService
    {
        string GenerateJwtToken(string email, ApplicationUser user);
    }
}
