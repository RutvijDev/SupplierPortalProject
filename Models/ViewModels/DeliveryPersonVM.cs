using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models.ViewModels
{
    public class DeliveryPersonVM
    {
        [Required(ErrorMessage = "Order ID is required!")]
        [MaxLength(15)]
        [DisplayName("Order ID")]
        public string? OrderID { get; set; }

        public string? CustomerName { get; set; }

        public string? ProductName { get; set; }
        
        public string? CompanyName { get; set; }
        
        public int? Quantity { get; set; }
                
        public decimal? TotalPrice { get; set; }
        
        public string? ShippingAddress { get; set; }
    }
}
