using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Models;
using ChatServer.Repositories;
using ChatServer.Services;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatServer.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _unitOfWork;
        
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IAccountService accountService,
            IUnitOfWork unitOfWork
        )
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                ApplicationUser user = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                var token = _accountService.GenerateJwtToken(model.UserName, user);
                return Ok(new AccessToken { Token = token, UserName = user.UserName, UserId = user.Id });
            }

            return NotFound("Wrong email or password");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterViewModel model)
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