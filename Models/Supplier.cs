using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class Supplier
    {
        [Key]
        [MaxLength(15)]
        public string? SupplierID { get; set; }

        [Required]
        [MaxLength(30)]
        public string? SupplierName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }

        [AllowNull]
        [ForeignKey("WareHouse")]
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

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
