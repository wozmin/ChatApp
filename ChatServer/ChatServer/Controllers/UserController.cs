using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IHostingEnvironment _environment;

        public UserController(IUserRepository userRepository, IMapper mapper,IHostingEnvironment environment)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ChatUserViewModel>>(await _userRepository.GetAllUsersAsync()));
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var userProfile = await _userRepository.GetUserProfile(id);
            if(userProfile == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserProfileViewModel>(userProfile));
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm(Name = "avatar")] IFormFile avatar)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (avatar.Length > 0)
            {
                
                using (var fileStream = new FileStream(Path.Combine(uploads, User.Identity.Name+"-avatar"), FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }
    }
}