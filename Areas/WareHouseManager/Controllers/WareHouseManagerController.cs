using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Areas.WareHouseManager.Controllers
{
    [Area("WareHouseManager")]
    [Authorize(Policy = "MustBelongToWareHouseManager")]

    public class WareHouseManagerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WareHouseManagerController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpcomingOrders()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var userName = User.Identity?.Name;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from supplierorder in _db.SupplierOrder
                         join order in _db.Order on supplierorder.OrderID equals order.OrderID
                         join product in _db.Product on supplierorder.ProductID equals product.ProductID
                         where (supplierorder.Supplier_Order_Status == "Shipped") && (order.WareHouseID == WareHouseID)
                         select new WareHouseManagerOrderVM
                         {
                             SupplierOrder = supplierorder,
                             Order = order,
                             Product = product,
                         };
            return View(orders.ToList());
        }

        [HttpPost]
        public IActionResult SupplierOrderUpdate(string id, string status)
        {
            var order = _db.SupplierOrder.FirstOrDefault(u => u.SupplierOrderID == id);

            if (status == "Delivered")
            {
                order.Supplier_Order_Status = status;
                order.Supplier_Delivered_Date = DateTime.Now;

                _db.SaveChanges();
                TempData["success"] = "Order Updated Successfully!";

                var userName = User.Identity?.Name;
                var WareHouseManager = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName);
                var WareHouseManagerOrderID = GenerateNextWareHouseManagerOrderID();
                
                var orders = _db.Order.FirstOrDefault(u => u.OrderID == order.OrderID);
                orders.WareHouseManagerID = WareHouseManager.WareHouseManagerID;

                var wareHouseManagerOrder = new WareHouseManagerOrder
                {
                    WareHouseOrderID = WareHouseManagerOrderID,
                    WareHouseManagerID = WareHouseManager.WareHouseManagerID,
                    OrderID = order.OrderID,
                    SupplierID = order.SupplierID,
                    ProductID = order.ProductID,
                    WareHouse_Received_Date = DateTime.Now,
                    WareHouse_Order_Status = "Pending",
                    Order_Delivered_Status = "Pending",
                    WareHouse_Shipment_Date = null,
                    Order_Delivered_Date = null,
                    DeliveryPersonID = null,
                };
                _db.WareHouseManagerOrder.Add(wareHouseManagerOrder);
                _db.SaveChanges();
            }

            return RedirectToAction("Orders");
        }

        private string GenerateNextWareHouseManagerOrderID()
        {
            string lastWareHouseManagerOrderID = _db.WareHouseManagerOrder.OrderByDescending(a => a.WareHouseOrderID).Select(a => a.WareHouseOrderID).FirstOrDefault();

            string nextWareHouseManagerOrderID;
            if (lastWareHouseManagerOrderID == null)
            {
                nextWareHouseManagerOrderID = "WO001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastWareHouseManagerOrderID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextWareHouseManagerOrderID = $"WO{nextNumericPart:D3}";
            }

            return nextWareHouseManagerOrderID;
        }

        public IActionResult CheckInOrders()
        {
            var userName = User.Identity?.Name;
            var WareHouseManagerID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseManagerID;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from wareHouseManagerOrder in _db.WareHouseManagerOrder
                         join order in _db.Order on wareHouseManagerOrder.OrderID equals order.OrderID
                         join product in _db.Product on wareHouseManagerOrder.ProductID equals product.ProductID
                         where (wareHouseManagerOrder.Order_Delivered_Status != "Delivered" && order.WareHouseID == WareHouseID) || (wareHouseManagerOrder.Order_Delivered_Status  == "Delivered" && EF.Functions.DateDiffDay(wareHouseManagerOrder.Order_Delivered_Date, DateTime.Today.Date) == 0 && order.WareHouseID == WareHouseID)
                         select new WareHouseManagerOrderVM
                         {
                            WareHouseManagerOrder = wareHouseManagerOrder,
                            Order = order,
                            Product = product,
                         };

            return View(orders.ToList());
        }

        public IActionResult PreviousDeliveredOrders()
        {
            var userName = User.Identity?.Name;
            var WareHouseManagerID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseManagerID;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from wareHouseManagerOrder in _db.WareHouseManagerOrder
                         join order in _db.Order on wareHouseManagerOrder.OrderID equals order.OrderID
                         join product in _db.Product on wareHouseManagerOrder.ProductID equals product.ProductID
                         where wareHouseManagerOrder.Order_Delivered_Status == "Delivered" && order.WareHouseID == WareHouseID
                         select new WareHouseManagerOrderVM
                         {
                             WareHouseManagerOrder = wareHouseManagerOrder,
                             Order = order,
                             Product = product,
                         };

            return View(orders.ToList());
        }

        public IActionResult UserProfile()
        {
            var userName = User.Identity?.Name;
            var userData = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName);

            var Data = new WareHouseManagerRegistrationVM
            {
                Id = userData?.Id,
                UserName = userData?.WareHouseManagerName,
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
            var userData = _db.WareHouseManager.FirstOrDefault(u => u.Id == id);

            var Data = new WareHouseManagerRegistrationVM
            {
                Id = id,
                UserName = userData?.WareHouseManagerName,
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
        public IActionResult UserProfileUpdate(WareHouseManagerRegistrationVM obj)
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

                var existingWareHouseManager = _db.WareHouseManager.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingWareHouseManager != null)
                {
                    existingWareHouseManager.WareHouseManagerName = obj.UserName;
                    existingWareHouseManager.Email = obj.Email;
                    existingWareHouseManager.FullName = obj.FullName;
                    existingWareHouseManager.MobileNumber = obj.PhoneNumber;
                    existingWareHouseManager.Password = obj.Password;
                }
                _db.Update(existingWareHouseManager);

                _db.SaveChanges();
                TempData["success"] = "Profile Updated Successfully!";
                return RedirectToAction("UserProfile", "WareHouseManager", new { area = "WareHouseManager" });
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var userName = User.Identity?.Name;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from supplierorder in _db.SupplierOrder
                         join order in _db.Order on supplierorder.OrderID equals order.OrderID
                         join product in _db.Product on supplierorder.ProductID equals product.ProductID
                         where (supplierorder.Supplier_Order_Status == "Shipped") && (order.WareHouseID == WareHouseID)
                         select new WareHouseManagerOrderVM
                         {
                             SupplierOrder = supplierorder,
                             Order = order,
                             Product = product,
                         };

            return Json(new { data = orders.ToList() });
        }

        public IActionResult GetAllPreviousDeliveredOrders()
        {
            var userName = User.Identity?.Name;
            var WareHouseManagerID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseManagerID;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from wareHouseManagerOrder in _db.WareHouseManagerOrder
                         join order in _db.Order on wareHouseManagerOrder.OrderID equals order.OrderID
                         join product in _db.Product on wareHouseManagerOrder.ProductID equals product.ProductID
                         where wareHouseManagerOrder.Order_Delivered_Status == "Delivered" && order.WareHouseID == WareHouseID
                         select new WareHouseManagerOrderVM
                         {
                             WareHouseManagerOrder = wareHouseManagerOrder,
                             Order = order,
                             Product = product,
                         };

            return Json(new { data = orders.ToList() });
        }

        public IActionResult GetAllCheckInOrders()
        {
            var userName = User.Identity?.Name;
            var WareHouseManagerID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseManagerID;
            var WareHouseID = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == userName)?.WareHouseID;

            var orders = from wareHouseManagerOrder in _db.WareHouseManagerOrder
                         join order in _db.Order on wareHouseManagerOrder.OrderID equals order.OrderID
                         join product in _db.Product on wareHouseManagerOrder.ProductID equals product.ProductID
                         where (wareHouseManagerOrder.Order_Delivered_Status != "Delivered" && order.WareHouseID == WareHouseID) || (wareHouseManagerOrder.Order_Delivered_Status == "Delivered" && EF.Functions.DateDiffDay(wareHouseManagerOrder.Order_Delivered_Date, DateTime.Today.Date) == 0 && order.WareHouseID == WareHouseID)
                         select new WareHouseManagerOrderVM
                         {
                             WareHouseManagerOrder = wareHouseManagerOrder,
                             Order = order,
                             Product = product,
                         };

            return Json(new { data = orders.ToList() });
        }

        #endregion

    }
}
