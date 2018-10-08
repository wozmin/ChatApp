using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Chat")]
    public class ChatController : Controller
    {
    }
}