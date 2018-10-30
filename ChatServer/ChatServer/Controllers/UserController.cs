using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Identity;
using ChatServer.Models;

namespace ChatServer.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper,IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page=1)
        {
            int pageSize = 6;
            return Ok(_mapper.Map<IEnumerable<ChatUserViewModel>>(await _unitOfWork.Users.GetAllUsersAsync(page,pageSize)));
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            var userProfile = await _unitOfWork.Users.GetUserProfile(id);
            if(userProfile == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserProfileViewModel>(userProfile));
        }

        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromBody]AvatarViewModel avatar)
        {
            var bytes = Convert.FromBase64String(avatar.Base64Avatar);
            var path = Path.Combine(_environment.WebRootPath, "Uploads");
            var user = await _unitOfWork.Users.GetUserByNameAsync(User.Identity.Name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var file = Path.Combine(path, $"{user.Id}-avatar.{avatar.Extention}");
            if (System.IO.File.Exists(file)) 
            {
                System.IO.File.Delete(file);
            }
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }

            }
            user.UserProfile.AvatarUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}//Uploads//{user.Id}-avatar.{avatar.Extention}";
            await _unitOfWork.SaveChangesAsync();
            return Ok("User avatar was saved");
        }

        [Authorize]
        [HttpDelete("avatar")]
        public async Task<IActionResult> DeleteAvatar()
        {
            var user = await _unitOfWork.Users.GetUserByNameAsync(User.Identity.Name);
            var extention = user.UserProfile.AvatarUrl.Substring(user.UserProfile.AvatarUrl.Length - 3);
            var path = Path.Combine(_environment.WebRootPath, "Uploads", $"{user.Id}-avatar.{extention}");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                user.UserProfile.AvatarUrl = null;
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            return NotFound("User avatar was not found");
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> EditUserProfile([FromBody]EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _unitOfWork.Users.GetUserByNameAsync(User.Identity.Name);
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.UserProfile.Address = model.Address;
            user.UserProfile.Age = model.Age;
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok("User data was updated");
        }
    }
}