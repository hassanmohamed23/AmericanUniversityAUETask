using System.ComponentModel.DataAnnotations;

namespace AmericanUniversityAUETask.Models
{
    public class SignUpViewModel
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

    }
}
