using ChatServer.Models;
using ChatServer.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Exceptions;
using Services.Factory;
using Services.Models;
using Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenFactory _jwtTokenFactory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="jwtTokenFactory">Jwt token factory</param>
        public AuthenticationService(IUnitOfWork unitOfWork, IJwtTokenFactory jwtTokenFactory)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenFactory = jwtTokenFactory;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <returns>Access token</returns>
        public async Task<AccessToken> AuthenticateAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            if (!result.Succeeded)
            {
                throw new ValidationException("User name or password are invalid");
            }
            var user = _userManager.Users.SingleOrDefault(r => r.UserName == userName);
            user.IsOnline = true;
            await _unitOfWork.SaveChangesAsync();
            var token = _jwtTokenFactory.GenerateJwt(userName, user);

            return new AccessToken { Token = token, UserName = user.UserName, UserId = user.Id };
        }
    }
}
