using ChatServer.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.ViewModel
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false )]
        [NotEmptyValidation]
        public string UserName { get; set; }
        [Required]
        [NotEmptyValidation]
        public string Password { get; set; }
    }
}
