using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CollegeProject.Models.ViewModels
{
    public class WareHouseManagerRegistrationVM
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "UserName is required!")]
        [MaxLength(15)]
        [DisplayName("UserName")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "FullName is required!")]
        [MaxLength(30)]
        [DisplayName("FulName")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [MaxLength(30)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Please enter a valid email address.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required!")]
        [MinLength(10, ErrorMessage = "Your phone number should be exactly 10 digits.")]
        [MaxLength(10)]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required!")]
        [DisplayName("Role")]
        [MaxLength(30)]
        public string? Role { get; set; }

        [Required]
        [DisplayName("WareHouse")]
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [RegularExpression("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Your password must be at least 8 characters long, with at least one digit, one lowercase letter, one uppercase letter, and one special character.")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password Must be same!")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        public string? ConfirmPassword { get; set; }
    }
}
