using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? CustomerID { get; set; }

        [Required]
        [MaxLength(15)]
        public string? CustomerName { get; set; }

        [AllowNull]
        [ForeignKey("WareHouse")]
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CustomerAddress { get; set; }

        [Required]
        [MaxLength(10)]
        public string? CustomerPhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string? CustomerEmail { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Password { get; set; }
    }
}
