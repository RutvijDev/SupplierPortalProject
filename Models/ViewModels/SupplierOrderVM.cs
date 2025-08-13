using CollegeProject.Models;

namespace CollegeProject.Models.ViewModels
{
    public class SupplierOrderVM
    {
        public Order? Order { get; set; }
        public SupplierOrder? SupplierOrder { get; set; }
        public Product? Product { get; set; }
    }
}
