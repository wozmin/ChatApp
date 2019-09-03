using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Filters;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Models;
using Services.Services;

namespace ChatServer.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [ApiController]
    [Route("api/account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Account controller constructor
        /// </summary>
        /// <param name="authenticationService">Authentication service</param>
        /// <param name="mapper">Auto mapper</param>
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

        /// <summary>
        /// Register user based on the provied credentials
        /// </summary>
        /// <param name="model">Register model <see cref="RegisterViewModel"/></param>
        /// <returns></returns>
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