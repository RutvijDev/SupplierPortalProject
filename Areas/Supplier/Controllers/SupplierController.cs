using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    [Authorize(Policy = "MustBelongToSupplier")]

    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SupplierController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductData()
        {
            var userName = User.Identity?.Name;
            var supplierData = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;
            List<Product> ProductData = _db.Product.Where(u => u.SupplierID == supplierData).ToList();
            return View(ProductData);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductRegistrationVM productData, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; /* to get path of wwwroot folder */
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); /*will give a random name for file*/
                    string productPath = Path.Combine(wwwRootPath, @"Images/ProductImages");

                    using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productData.ProductImage = @"\Images\ProductImages\" + fileName;
                }

                var productID = GenerateNextProductID();
                var userName = User.Identity?.Name;
                var supplierID = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;
                var dateAdded = DateOnly.FromDateTime(DateTime.Now);
                var dateModified = DateOnly.FromDateTime(DateTime.Now);
                var status = productData.ProductStatus;
                if (productData.ProductQuantity == 0 && productData.ProductStatus == "Available")
                {
                    status = "Not Available";
                }
                else if (productData.ProductQuantity > 0 && productData.ProductStatus == "Not Available")
                {
                    status = "Availabel";
                }

                var Product = new Product
                {
                    ProductID = productID,
                    SupplierID = supplierID,
                    ProductName = productData.ProductName,
                    CompanyName = productData.CompanyName,
                    ProductCategory = productData.ProductCategory,
                    ProductDescription = productData.ProductDescription,
                    ProductPrice = productData.ProductPrice,
                    ProductQuantity = productData.ProductQuantity,
                    ProductImage = productData.ProductImage,
                    ProductDateAdded = dateAdded,
                    ProductLast_Modified = dateModified,
                    ProductStatus = status,
                };

                _db.Product.Add(Product);
                _db.SaveChanges();
                TempData["success"] = "Product Added Successfully!";

                return RedirectToAction("Productdata", "Supplier");
            }

            return View();
        }

        private string GenerateNextProductID()
        {
            string lastProductID = _db.Product.OrderByDescending(a => a.ProductID).Select(a => a.ProductID).FirstOrDefault();

            string nextProductID;
            if (lastProductID == null)
            {
                nextProductID = "P001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastProductID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextProductID = $"P{nextNumericPart:D3}";
            }

            return nextProductID;
        }

        public IActionResult Edit(string id)
        {
            var productData = _db.Product.FirstOrDefault(u => u.ProductID == id);
            var product = new ProductRegistrationVM
            {
                ProductID = productData.ProductID,
                SupplierID = productData.SupplierID,
                ProductName = productData.ProductName,
                CompanyName = productData.CompanyName,
                ProductCategory = productData.ProductCategory,
                ProductDescription = productData.ProductDescription,
                ProductPrice = productData.ProductPrice,
                ProductQuantity = productData.ProductQuantity,
                ProductImage = productData.ProductImage,
                ProductDateAdded = productData.ProductDateAdded,
                ProductLast_Modified = productData.ProductLast_Modified,
                ProductStatus = productData.ProductStatus,
            };

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductRegistrationVM productData, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; /* to get path of wwwroot folder */
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); /*will give a random name for file*/
                    string productPath = Path.Combine(wwwRootPath, @"Images/ProductImages");

                    if (!string.IsNullOrEmpty(productData.ProductImage))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productData.ProductImage.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productData.ProductImage = @"\Images\ProductImages\" + fileName;
                }

                var status = productData.ProductStatus;
                if (productData.ProductQuantity == 0 && productData.ProductStatus == "Available")
                {
                    status = "Not Available";
                }
                else if (productData.ProductQuantity > 0 && productData.ProductStatus == "Not Available")
                {
                    status = "Available";
                }

                var existingProduct = _db.Product.AsNoTracking().FirstOrDefault(u => u.ProductID == productData.ProductID);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = productData.ProductName;
                    existingProduct.CompanyName = productData.CompanyName;
                    existingProduct.ProductCategory = productData.ProductCategory;
                    existingProduct.ProductDescription = productData.ProductDescription;
                    existingProduct.ProductPrice = productData.ProductPrice;
                    existingProduct.ProductQuantity = productData.ProductQuantity;
                    existingProduct.ProductImage = productData.ProductImage;
                    existingProduct.ProductLast_Modified = DateOnly.FromDateTime(DateTime.Now);
                    existingProduct.ProductStatus = status;
                };

                _db.Product.Update(existingProduct);
                _db.SaveChanges();
                TempData["success"] = "Product Updated Successfully!";
                return RedirectToAction("ProductData", "Supplier");
            }
            return View();
        }

        public IActionResult Delete(string? id)
        {
            var productData = _db.Product.FirstOrDefault(u => u.ProductID == id);
            var product = new ProductRegistrationVM
            {
                ProductID = productData.ProductID,
                SupplierID = productData.SupplierID,
                ProductName = productData.ProductName,
                CompanyName = productData.CompanyName,
                ProductCategory = productData.ProductCategory,
                ProductDescription = productData.ProductDescription,
                ProductPrice = productData.ProductPrice,
                ProductQuantity = productData.ProductQuantity,
                ProductImage = productData.ProductImage,
                ProductDateAdded = productData.ProductDateAdded,
                ProductLast_Modified = productData.ProductLast_Modified,
                ProductStatus = productData.ProductStatus,
            };

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(string? id)
        {
            var productData = _db.Product.FirstOrDefault(u => u.ProductID == id);

            string wwwRootPath = _webHostEnvironment.WebRootPath; /* to get path of wwwroot folder */

            if (!string.IsNullOrEmpty(productData.ProductImage))
            {
                var oldImagePath = Path.Combine(wwwRootPath, productData.ProductImage.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _db.Product.Remove(productData);
            _db.SaveChanges();
            TempData["success"] = "Product Deleted Successfully!";
            return RedirectToAction("ProductData", "Supplier");
        }

        public IActionResult OrderList()
        {
            var userName = User.Identity?.Name;
            var supplierID = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;

            if (supplierID == null)
            {
                return NotFound();
            }
            var orders = _db.Order.Where(u => u.WareHouseManagerID == null).ToList();
            var supplierOrderID = GenerateNextSupplierOrderID();

            foreach (var order in orders)
            {
                if (order.SupplierID == supplierID)
                {
                    var orderID = order.OrderID;
                    var existingOrder = _db.SupplierOrder.AsNoTracking().FirstOrDefault(so => so.OrderID == orderID);

                    if (existingOrder == null)
                    {
                        var supplierOrder = new SupplierOrder
                        {
                            SupplierOrderID = supplierOrderID,
                            OrderID = orderID,
                            SupplierID = supplierID,
                            ProductID = order.ProductID,
                            Supplier_Order_Status = "Pending",
                            Supplier_Ship_Date = null,
                            Supplier_Delivered_Date = null
                        };

                        _db.SupplierOrder.Add(supplierOrder);
                        _db.SaveChanges();
                        supplierOrderID = GenerateNextSupplierOrderID();
                    }
                }
            }

            var today = DateTime.Today.Date;
            var supplierOrders = from supplierorder in _db.SupplierOrder
                                 join order in _db.Order on supplierorder.OrderID equals order.OrderID
                                 join product in _db.Product on supplierorder.ProductID equals product.ProductID
                                 where (supplierID == supplierorder.SupplierID && supplierorder.Supplier_Order_Status != "Delivered") || (supplierorder.Supplier_Order_Status == "Delivered" && EF.Functions.DateDiffDay(supplierorder.Supplier_Ship_Date, today) == 0)
                                 select new SupplierOrderVM
                                 {
                                     Order = order,
                                     SupplierOrder = supplierorder,
                                     Product = product
                                 };

            if (supplierOrders == null)
            {
                return NotFound();
            }

            return View(supplierOrders.ToList());
        }

        [HttpPost]
        public IActionResult OrderUpdate(string id, string status)
        {
            var order = _db.SupplierOrder.FirstOrDefault(u => u.SupplierOrderID == id);

            if (order != null)
            {
                order.Supplier_Order_Status = status;
                if (status == "Shipped")
                {
                    order.Supplier_Ship_Date = DateTime.Now;
                }
                _db.SupplierOrder.Update(order);
                _db.SaveChanges();
                TempData["success"] = "Order Updated Successfully!";
                return RedirectToAction("OrderList");
            }
            return NotFound();
        }

        private string GenerateNextSupplierOrderID()
        {
            string lastSupplierOrderID = _db.SupplierOrder.OrderByDescending(a => a.SupplierOrderID).Select(a => a.SupplierOrderID).FirstOrDefault();

            string nextSupplierOrderID;
            if (lastSupplierOrderID == null)
            {
                nextSupplierOrderID = "SO001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastSupplierOrderID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextSupplierOrderID = $"SO{nextNumericPart:D3}";
            }

            return nextSupplierOrderID;
        }

        public IActionResult PreviousOrders()
        {
            var userName = User.Identity?.Name;
            var supplierID = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;

            if (supplierID == null)
            {
                return NotFound();
            }
            var today = DateTime.Today.Date;
            var supplierOrders = from supplierOrder in _db.SupplierOrder
                                 join order in _db.Order on supplierOrder.OrderID equals order.OrderID
                                 join product in _db.Product
                                 on order.ProductID equals product.ProductID
                                 where supplierID == order.SupplierID && supplierOrder.Supplier_Order_Status == "Delivered" && EF.Functions.DateDiffDay(supplierOrder.Supplier_Delivered_Date, today) != 0
                                 select new SupplierOrderVM
                                 {
                                     Order = order,
                                     SupplierOrder = supplierOrder,
                                     Product = product
                                 };

            if (supplierOrders == null)
            {
                return NotFound();
            }

            return View(supplierOrders.ToList());
        }

        public IActionResult UserProfile()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName);

            var Data = new SupplierRegistrationVM
            {
                Id = userData?.Id,
                UserName = userData?.SupplierName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                WareHouseID = userData?.WareHouseID,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };

            return View(Data);
        }

        public IActionResult UserProfileUpdate(int id)
        {
            var userData = _db.Supplier.FirstOrDefault(u => u.Id == id);

            var Data = new SupplierRegistrationVM
            {
                Id = id,
                UserName = userData?.SupplierName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                WareHouseID = userData?.WareHouseID,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };

            return View(Data);
        }

        [HttpPost]
        public IActionResult UserProfileUpdate(SupplierRegistrationVM obj)
        {
            var id = _db.User.AsNoTracking().FirstOrDefault(u => u.UserName == obj.UserName)?.Id;
            if (id != null)
            {
                if (id != obj.Id)
                {
                    TempData["Error"] = "Username already exists! Please choose a different username!";
                    return View(obj);
                }
            }

            if (ModelState.IsValid)
            {
                var existingUser = _db.User.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingUser != null)
                {
                    existingUser.FullName = obj.FullName;
                    existingUser.UserName = obj.UserName;
                    existingUser.Email = obj.Email;
                    existingUser.PhoneNumber = obj.PhoneNumber;
                    existingUser.Password = obj.Password;
                }

                _db.User.Update(existingUser);

                var existingSupplier = _db.Supplier.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingSupplier != null)
                {
                    existingSupplier.SupplierName = obj.UserName;
                    existingSupplier.Email = obj.Email;
                    existingSupplier.FullName = obj.FullName;
                    existingSupplier.MobileNumber = obj.PhoneNumber;
                    existingSupplier.Password = obj.Password;
                    existingSupplier.WareHouseID = obj.WareHouseID;
                }
                _db.Update(existingSupplier);

                _db.SaveChanges();
                TempData["success"] = "Profile Updated Successfully!";
                return RedirectToAction("UserProfile", "Supplier", new { area = "Supplier" });
            }
            return View();
        }
    

        #region API CALLS

        [HttpGet]
        public IActionResult GetAllOrderList()
        {
            var userName = User.Identity?.Name;
            var supplierID = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;

            var today = DateTime.Today.Date;
            var supplierOrders = from supplierorder in _db.SupplierOrder
                                 join order in _db.Order on supplierorder.OrderID equals order.OrderID
                                 join product in _db.Product on supplierorder.ProductID equals product.ProductID
                                 where (supplierID == supplierorder.SupplierID && supplierorder.Supplier_Order_Status != "Delivered") || (supplierorder.Supplier_Order_Status == "Delivered" && EF.Functions.DateDiffDay(supplierorder.Supplier_Ship_Date, today) == 0)
                                 select new SupplierOrderVM
                                 {
                                     Order = order,
                                     SupplierOrder = supplierorder,
                                     Product = product
                                 };

            return Json(new { data = supplierOrders.ToList() });
        }

        [HttpGet]
        public IActionResult GetAllPreviousOrders()
        {
            var userName = User.Identity?.Name;
            var supplierID = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;

            var today = DateTime.Today.Date;
            var supplierOrders = from supplierOrder in _db.SupplierOrder
                                 join order in _db.Order on supplierOrder.OrderID equals order.OrderID
                                 join product in _db.Product
                                 on order.ProductID equals product.ProductID
                                 where supplierID == order.SupplierID && supplierOrder.Supplier_Order_Status == "Delivered" && EF.Functions.DateDiffDay(supplierOrder.Supplier_Delivered_Date, today) != 0
                                 select new SupplierOrderVM
                                 {
                                     Order = order,
                                     SupplierOrder = supplierOrder,
                                     Product = product
                                 };

            return Json(new { data = supplierOrders.ToList() });
        }

        [HttpGet]
        public IActionResult GetAllProductData()
        {
            var userName = User.Identity?.Name;
            var supplierData = _db.Supplier.FirstOrDefault(u => u.SupplierName == userName)?.SupplierID;
            List<Product> ProductData = _db.Product.Where(u => u.SupplierID == supplierData).ToList();

            return Json(new { data = ProductData });
        }

        #endregion
    }
}
