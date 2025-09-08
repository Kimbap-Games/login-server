using System.ComponentModel.DataAnnotations;

namespace LoginServer.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}