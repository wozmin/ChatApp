using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Models;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ChatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async  Task<IActionResult> GetChats(string id)
        {
            return Ok(_mapper.Map<IEnumerable<ChatViewModel>>(await _unitOfWork.Chats.GetChatsByUser(id)));
        }

        [HttpGet("{id}/message")]
        public async Task<IActionResult> GetChatMessages(Guid id,int page=1)
        {
            int pageSize = 20;
            if(await _unitOfWork.Chats.GetByIdAsync(id) == null)
            {
                return NotFound("Chat was not found");
            }
            return Ok(_mapper.Map<IEnumerable<ChatMessageModel>>(await _unitOfWork.Chats.GetChatMessagesAsync(id,page,pageSize)));
        }

        [HttpGet("{id}/member")]
        public async Task<IActionResult> GetChatMembers(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(id);
            if(chat == null)
            {
                return BadRequest("Chat not found");
            }
            return Ok(_mapper.Map<IEnumerable<ChatUserViewModel>>(await _unitOfWork.Chats.GetChatUsersAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(string chatName)
        {
            var creator = await _unitOfWork.Users.GetUserByNameAsync(User.Identity.Name);
            var admin = await _unitOfWork.Users.GetUserByNameAsync("admin");
            var currentUser = await _unitOfWork.Users.GetUserByNameAsync(User.Identity.Name);
            var chat = new Chat()
            {
                Name = chatName,
                Creator = creator,
                CreationDate = DateTime.Now,
            };
            _unitOfWork.Chats.Create(chat);
            chat.UserChats = new List<UserChat>
            {
                new UserChat
                {
                    ChatId = chat.Id,
                    ApplicationUserId = currentUser.Id
                }
            };
            chat.Messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Message = "Chat was created ",
                    Date = DateTime.Now,
                    ChatId = chat.Id,
                    UserId = admin.Id
                }
            };
            await _unitOfWork.SaveChangesAsync();
            return Ok(_mapper.Map<ChatViewModel>(chat));
        }
    }
}