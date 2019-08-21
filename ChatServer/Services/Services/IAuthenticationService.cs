using Services.Dto;
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

        /// <summary>
        /// Registers new user in the system
        /// </summary>
        /// <param name="model">Model <see cref="RegisterDto"/></param>
        /// <returns>Access token <see cref="AccessToken"/></returns>
        Task<AccessToken> RegisterAsync(RegisterDto model);
    }
}
