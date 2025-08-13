using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Policy = "MustBelongToCustomer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomerController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var products = (from product in _db.Product
                            join supplier in _db.Supplier on product.SupplierID equals supplier.SupplierID
                            join customer in _db.Customer on supplier.WareHouseID equals customer.WareHouseID
                            where userData.WareHouseID == supplier.WareHouseID
                            select product)
                           .Distinct();

            return View(products.ToList());
        }

        public IActionResult ViewProduct(string id)
        {
            var product = _db.Product.FirstOrDefault(u => u.ProductID == id);

            return View(product);
        }

        public IActionResult BuyProduct(string id)
        {
            var product = _db.Product.FirstOrDefault(u => u.ProductID == id);

            var productData = new OrderProductVM
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CompanyName = product.CompanyName,
                ProductCategory = product.ProductCategory,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductImage = product.ProductImage,
                ProductStatus = product.ProductStatus,
                PurchaseQuantity = null,
                TotalPrice = null,
            };

            return View(productData);
        }

        [HttpPost]
        public IActionResult BuyProduct(OrderProductVM obj)
        {
            return RedirectToAction("ConfirmOrder", new { id = obj.ProductID, quantity = obj.PurchaseQuantity });
        }

        public IActionResult ConfirmOrder(string id, int quantity)
        {
            var order = _db.Product.FirstOrDefault(u => u.ProductID == id);

            var orderdata = new OrderProductVM
            {
                ProductID = order.ProductID,
                ProductName = order.ProductName,
                CompanyName = order.CompanyName,
                ProductCategory = order.ProductCategory,
                ProductDescription = order.ProductDescription,
                ProductPrice = order.ProductPrice,
                ProductImage = order.ProductImage,
                ProductStatus = order.ProductStatus,
                PurchaseQuantity = quantity,
                TotalPrice = order.ProductPrice * quantity
            };

            return View(orderdata);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderProductVM obj) 
        {
            var product = _db.Product.FirstOrDefault(u => u.ProductID == obj.ProductID);

            var supplier = _db.Supplier.FirstOrDefault(u => u.SupplierID == product.SupplierID);

            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var order = new Order
            {
                OrderID = GenerateNextOrderID(),
                CustomerID = userData.CustomerID,
                CustomerName = userName,
                OrderDate = DateTime.Now,
                ProductID = product.ProductID,
                SupplierID = product.SupplierID,
                WareHouseManagerID = null,
                WareHouseID = supplier.WareHouseID,
                Quantity = obj.PurchaseQuantity,
                UnitPrice = product.ProductPrice,
                TotalPrice = obj.TotalPrice,
                ShippingAddress = userData.CustomerAddress,
                OrderStatus = "Pending",
                Order_Delivered_Date = null,
                DeliveryPersonID = null
            };

            _db.Order.Add(order);
            _db.SaveChanges();
            TempData["success"] = "Order Placed Succeffuly!";

            return RedirectToAction("Products");
        }

        private string GenerateNextOrderID()
        {
            string lastOrderID = _db.Order.OrderByDescending(a => a.OrderID).Select(a => a.OrderID).FirstOrDefault();

            string nextOrderID;
            if (lastOrderID == null)
            {
                nextOrderID = "P001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastOrderID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextOrderID = $"O{nextNumericPart:D3}";
            }

            return nextOrderID;
        }

        public IActionResult Orders()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var orderdata = from order in _db.Order
                            join product in _db.Product on order.ProductID equals product.ProductID
                            where order.CustomerID == userData.CustomerID && order.OrderStatus != "Delivered"
                            select new OrderListVM
                            {
                                Order = order,
                                Product = product
                            };

            return View(orderdata.ToList());
        }

        public IActionResult PreviousOrders()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var orderdata = from order in _db.Order
                            join product in _db.Product on order.ProductID equals product.ProductID
                            where order.CustomerID == userData.CustomerID && order.OrderStatus == "Delivered"
                            select new OrderListVM
                            {
                                Order = order,
                                Product = product
                            };

            return View(orderdata.ToList());
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var orderdata = from order in _db.Order
                            join product in _db.Product on order.ProductID equals product.ProductID
                            where order.CustomerID == userData.CustomerID && order.OrderStatus != "Delivered"
                            select new OrderListVM
                            {
                                Order = order,
                                Product = product
                            };

            return Json(new { data = orderdata.ToList() });
        }

        [HttpGet]
        public IActionResult GetAllPreviousOrders()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var orderdata = from order in _db.Order
                            join product in _db.Product on order.ProductID equals product.ProductID
                            where order.CustomerID == userData.CustomerID && order.OrderStatus == "Delivered"
                            select new OrderListVM
                            {
                                Order = order,
                                Product = product
                            };

            return Json(new { data = orderdata.ToList() });
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Customer.FirstOrDefault(u => u.CustomerName == userName);

            var products = (from product in _db.Product
                            join supplier in _db.Supplier on product.SupplierID equals supplier.SupplierID
                            join customer in _db.Customer on supplier.WareHouseID equals customer.WareHouseID
                            where userData.WareHouseID == supplier.WareHouseID
                            select product)
                           .Distinct();

            return Json(new { data = products.ToList() });
        }

        #endregion
    }
}
