using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CollegeProject.Models
{
    public class WareHouseManagerOrder
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? WareHouseOrderID { get; set; }

        [ForeignKey("WareHouseManager")]
        [Required]
        [MaxLength(15)]
        public string? WareHouseManagerID { get; set; }

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
        public DateTime WareHouse_Received_Date { get; set; }

        [Required]
        [MaxLength(15)]
        public string? WareHouse_Order_Status { get; set; }

        [AllowNull]
        public DateTime? WareHouse_Shipment_Date { get; set; }

        [Required]
        [MaxLength(15)]
        public string? Order_Delivered_Status { get; set; }

        [AllowNull]
        public DateTime? Order_Delivered_Date { get; set; }

        [AllowNull]
        [MaxLength(15)]
        public string? DeliveryPersonID { get; set; }

    }
}
