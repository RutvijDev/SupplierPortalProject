using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models.ViewModels
{
    public class ProductRegistrationVM
    {
        [MaxLength(15)]
        public string? ProductID { get; set; }

        [MaxLength(15)]
        public string? SupplierID { get; set; }

        [Required(ErrorMessage = "ProductName is required!")]
        [DisplayName("Product Name")]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "CompanyName is required!")]
        [DisplayName("CompanyName")]
        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Product Category is required!")]
        [DisplayName("Product Category")]
        [MaxLength(50)]
        public string? ProductCategory { get; set; }

        [Required(ErrorMessage = "Product Description is required!")]
        [DisplayName("Product Description")]
        [MaxLength(300)]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "Product Price is required!")]
        [DisplayName("Product Price")]
        [DataType(DataType.Currency)]
        public decimal? ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Quantity is required!")]
        [DisplayName("Product Quantity")]
        public int? ProductQuantity { get; set; }

        [ValidateNever]
        public string? ProductImage { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? ProductDateAdded { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? ProductLast_Modified { get; set; }

        [Required(ErrorMessage = "Product Status is required!")]
        [DisplayName("Product Status")]
        public string? ProductStatus { get; set; }
    }
}
