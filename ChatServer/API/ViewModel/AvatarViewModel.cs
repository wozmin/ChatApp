using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class AvatarViewModel
    {
        public string Base64Avatar { get; set; }
        public string Extention { get; set; }
    }
}
