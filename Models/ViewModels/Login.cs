using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models.ViewModels
{
    public class Login
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
