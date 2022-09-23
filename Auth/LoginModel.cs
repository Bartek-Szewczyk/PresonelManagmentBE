using System.ComponentModel.DataAnnotations;

namespace PresonelManagmentBE.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}