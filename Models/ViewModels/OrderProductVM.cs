namespace CollegeProject.Models.ViewModels
{
    public class OrderProductVM
    {
        public string? ProductID { get; set; }  

        public string? ProductName { get; set; }

        public string? CompanyName { get; set; }

        public string? ProductCategory { get; set; }

        public string? ProductDescription { get; set; }

        public decimal? ProductPrice { get; set; }

        public string? ProductImage { get; set; }

        public string? ProductStatus { get; set; }

        public int? PurchaseQuantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
