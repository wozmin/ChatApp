using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatServer.Repositories;
using ChatServer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [Produces("application/json")]
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ChatController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllChats()
        {
            return Ok( _mapper.Map<IEnumerable<ChatViewModel>>(await _unitOfWork.Chats.GetAll()));
        }

        [HttpGet("{id}/message")]
        public async Task<IActionResult> GetChatMessages(int id)
        {
            var chat = await _unitOfWork.Chats.GetById(id);
            if(chat == null)
            {
                return NotFound("Chat was not found");
            }
            return Ok(_mapper.Map<IEnumerable<ChatMessageModel>>(chat.Messages));
        }

        [HttpGet("{id}/member")]
        public async Task<IActionResult> GetChatMembers(int id)
        {
            var chat = await _unitOfWork.Chats.GetById(id);
            if(chat == null)
            {
                return BadRequest("Chat not found");
            }
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(await _unitOfWork.Chats.GetChatUsers(id)));
        }
    }
}