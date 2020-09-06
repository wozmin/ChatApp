using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.ViewModel
{
    public class EditUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
