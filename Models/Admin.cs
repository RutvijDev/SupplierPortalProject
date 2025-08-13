using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class Admin
    {
        [Key]
        [MaxLength(15)]
        public string? AdminID { get; set; }

        [Required]
        [MaxLength(30)]
        public string? AdminName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(10)]
        public string? MobileNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }


        [Required]
        [MaxLength(15)]
        public string? Role { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Password { get; set; }

        [Required]
        [ForeignKey("User")]
        public int? Id { get; set; }
    }
}
