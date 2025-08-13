using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CollegeProject.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }

        [Required]
        [MinLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Role { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Password { get; set; }
    }
}
