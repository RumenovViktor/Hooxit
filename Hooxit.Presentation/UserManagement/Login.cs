using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.UserManagement
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
