using ChatServer.Models;
using ChatServer.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Dto;
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

        /// <summary>
        /// Registers new user in the system
        /// </summary>
        /// <param name="model">Model <see cref="RegisterDto"/></param>
        /// <returns>Access token <see cref="AccessToken"/></returns>
        public async Task<AccessToken> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var errorMsg = result.Errors
                    .Select(error => error.Description)
                    .FirstOrDefault();
                throw new ValidationException(errorMsg);
            }

            await _signInManager.SignInAsync(user, false);

            user.UserProfile = new UserProfile
            {
                Age = model.Age,
                Address = model.Address,
            };

            await _unitOfWork.SaveChangesAsync();

            var token = _jwtTokenFactory.GenerateJwt(model.UserName, user);
            var accessToken = new AccessToken { Token = token, UserName = model.UserName, UserId = user.Id };

            return accessToken;
        }
    }
}
