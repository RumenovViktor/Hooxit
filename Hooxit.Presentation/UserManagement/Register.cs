using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.UserManagement
{
    public class Register
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
