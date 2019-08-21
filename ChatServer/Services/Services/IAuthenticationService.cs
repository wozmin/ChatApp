using Services.Models;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <returns>Access token</returns>
        Task<AccessToken> AuthenticateAsync(string userName, string password);
    }
}
