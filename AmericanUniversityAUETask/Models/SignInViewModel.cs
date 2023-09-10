using System.ComponentModel.DataAnnotations;

namespace AmericanUniversityAUETask.Models
{
    public class SignInViewModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
