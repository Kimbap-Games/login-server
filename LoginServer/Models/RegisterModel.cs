using System.ComponentModel.DataAnnotations;

namespace LoginServer.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}