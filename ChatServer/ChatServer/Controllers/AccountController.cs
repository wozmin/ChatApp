using System.Threading.Tasks;
using ChatServer.Models;
using ChatServer.Repositories;
using ChatServer.Services;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Services;

namespace ChatServer.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService accountService,
            IAuthenticationService authenticationService,
            IUnitOfWork unitOfWork
        )
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticate user based on the provied credentials
        /// </summary>
        /// <param name="model">Login model <see cref="LoginViewModel"/></param>
        /// <returns>Access token <see cref="AccessToken"/></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            return Ok(await _authenticationService.AuthenticateAsync(model.UserName, model.Password));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                user.UserProfile = new UserProfile
                {
                    Age = model.Age,
                    Address = model.Address,
                };

                var token = _accountService.GenerateJwtToken(model.UserName, user);

                var accessToken = new AccessToken { Token = token, UserName = model.UserName, UserId = user.Id };

                await _unitOfWork.SaveChangesAsync();
                
                return Ok(accessToken);
            }

            return BadRequest(result.Errors);
        }
    }
}