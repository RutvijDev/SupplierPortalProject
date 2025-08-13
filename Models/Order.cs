using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class Order
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? OrderID { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [MaxLength(15)]
        public string? CustomerID { get; set; }

        [Required]
        [MaxLength(15)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(15)]
        [ForeignKey("Product")]
        public string? ProductID { get; set; }

        [Required]
        [MaxLength(15)]
        [ForeignKey("Supplier")]
        public string? SupplierID { get; set; }

        [AllowNull]
        [MaxLength(15)]
        public string? WareHouseManagerID { get; set; }

        [AllowNull]
        [MaxLength(15)]
        public string? DeliveryPersonID { get; set; }

        [AllowNull]
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }

        [Required]
        public decimal? TotalPrice { get; set; }

        [Required]
        [MaxLength(100)]
        public string? ShippingAddress { get; set; }

        [Required]
        [MaxLength(15)]
        public string? OrderStatus { get; set; }

        [AllowNull]
        public DateTime? Order_Delivered_Date { get; set; }
    }
}
