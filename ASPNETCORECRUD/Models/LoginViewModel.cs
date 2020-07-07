using System.ComponentModel.DataAnnotations;

namespace ASPNETCORECRUD.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Name cannot be blank")]
        [MaxLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Make sure you enter a password")]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}
