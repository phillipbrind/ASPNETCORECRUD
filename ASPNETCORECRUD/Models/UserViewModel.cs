using System.ComponentModel.DataAnnotations;

namespace ASPNETCORECRUD.Models
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name cannot be blank")]
        [MaxLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Make sure you select a gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Make sure you select a color")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Make sure you enter a password")]
        [MaxLength(10)]
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}
