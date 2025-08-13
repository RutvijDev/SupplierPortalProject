using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models
{
    public class Product
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? ProductID { get; set; }

        [ForeignKey("Supplier")]
        [Required]
        [MaxLength(15)]
        public string? SupplierID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProductCategory { get; set; }

        [Required]
        [MaxLength(300)]
        public string? ProductDescription { get; set; }

        [Required]
        public decimal? ProductPrice { get; set; }

        [Required]
        public int? ProductQuantity { get; set; }

        [ValidateNever]
        public string? ProductImage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? ProductDateAdded { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? ProductLast_Modified { get; set; }

        [Required]
        [MaxLength(15)]
        public string? ProductStatus { get; set; }
    }
}
