using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Filters;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
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
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AccountController(
            IAuthenticationService authenticationService,
            IMapper mapper
        )
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticate user based on the provied credentials
        /// </summary>
        /// <param name="model">Login model <see cref="LoginViewModel"/></param>
        /// <returns>Access token <see cref="AccessToken"/></returns>
        [AllowAnonymous]
        [ValidationFilter]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            return Ok(await _authenticationService.AuthenticateAsync(model.UserName, model.Password));
        }

        [AllowAnonymous]
        [ValidationFilter]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            var registerDto = _mapper.Map<RegisterDto>(model);
            return Ok(await _authenticationService.RegisterAsync(registerDto));
        }
    }
}