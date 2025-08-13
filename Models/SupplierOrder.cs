using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class SupplierOrder
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? SupplierOrderID { get; set; }

        [ForeignKey("Order")]
        [Required]
        [MaxLength(15)]
        public string? OrderID { get; set; }

        [ForeignKey("Supplier")]
        [Required]
        [MaxLength(15)]
        public string? SupplierID { get; set; }

        [ForeignKey("ProductID")]
        [Required]
        [MaxLength(15)]
        public string? ProductID { get; set; }

        [Required]
        [MaxLength(15)]
        public string? Supplier_Order_Status { get; set; }

        [AllowNull]
        public DateTime? Supplier_Ship_Date { get; set; } 

        [AllowNull]
        public DateTime? Supplier_Delivered_Date { get; set; }
    }
}
