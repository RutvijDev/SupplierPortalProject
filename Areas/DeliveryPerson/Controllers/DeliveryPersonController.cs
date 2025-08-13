using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Areas.DeliveryPerson.Controllers
{
    [Area("DeliveryPerson")]
    [Authorize(Policy = "MustBelongToDeliveryPerson")]
    public class DeliveryPersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DeliveryPersonController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FindDeliveryOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindDeliveryOrder(string OrderID)
        {
            var userName = User.Identity?.Name;
            var deliveryPerson = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == userName);

            var order = _db.Order.FirstOrDefault(o => o.WareHouseID == deliveryPerson.WareHouseID && o.OrderID == OrderID);

            if (order == null || order.WareHouseManagerID == null || order.DeliveryPersonID != null)
            {
                TempData["Error"] = "Order Not Found!";
                return View("FindDeliveryOrder");
            }

            return RedirectToAction("DeliveryOrderData", new {id =  order.OrderID});         
        }

        public IActionResult DeliveryOrderData(string id)
        {
            var order = _db.Order.FirstOrDefault(u => u.OrderID == id);
            var product = _db.Product.FirstOrDefault(u => u.ProductID == order.ProductID);

            var orderdata = new DeliveryPersonVM
            {
                OrderID = id,
                CustomerName = order.CustomerName,
                ProductName = product.ProductName,
                CompanyName = product.CompanyName,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
            };

            return View(orderdata);
        }

        [HttpPost,ActionName("DeliveryOrderData")]
        public IActionResult UpdateOrder(string id)
        {
            var order = _db.Order.FirstOrDefault(u => u.OrderID == id);

            var userName = User.Identity?.Name;
            var deliveryPersonID = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == userName)?.DeliveryPersonID;

            order.DeliveryPersonID = deliveryPersonID;
            order.OrderStatus = "Shipped";

            var wareHouseOrder = _db.WareHouseManagerOrder.FirstOrDefault(u => u.OrderID == id);
            wareHouseOrder.WareHouse_Order_Status = "Shipped";
            wareHouseOrder.Order_Delivered_Status = "Shipped";
            wareHouseOrder.WareHouse_Shipment_Date = DateTime.Now;
            wareHouseOrder.DeliveryPersonID = deliveryPersonID;

            _db.SaveChanges();

            TempData["Success"] = "Order Accepted Successfully!";
            return RedirectToAction("OrderForDelivery");
        }

        public IActionResult OrderForDelivery()
        {
            var username = User.Identity?.Name;
            var deliveryPersonID = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == username)?.DeliveryPersonID;

            var orders = from order in _db.Order
                         join product in _db.Product on order.ProductID equals product.ProductID
                         where order.DeliveryPersonID == deliveryPersonID && order.OrderStatus == "Shipped"
                         select new DeliveryPersonOrderVM
                         {
                             Order = order,
                             Product = product,
                         };

            return View(orders.ToList());
        }

        [HttpPost]
        public IActionResult OrderStatusUpdate(string id, string status)
        {
            var order = _db.Order.FirstOrDefault(u => u.OrderID == id);

            order.OrderStatus = status;

            var wareHouseOrder = _db.WareHouseManagerOrder.FirstOrDefault(u => u.OrderID == id);

            wareHouseOrder.Order_Delivered_Status = status;
            _db.SaveChanges();

            if(status == "Delivered")
            {
                wareHouseOrder.Order_Delivered_Date = DateTime.Now;
                order.Order_Delivered_Date = DateTime.Now;
                _db.SaveChanges();
                TempData["success"] = "Order Delivered Successfully!";
            }

            return RedirectToAction("OrderForDelivery");
        }

        public IActionResult PreviouslyDeliveredOrder()
        {
            var username = User.Identity?.Name;
            var deliveryPerson = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == username);

            var orders = from order in _db.Order
                         join product in _db.Product on order.ProductID equals product.ProductID
                         where order.DeliveryPersonID == deliveryPerson.DeliveryPersonID && order.OrderStatus == "Delivered"
                         select new DeliveryPersonOrderVM
                         {
                             Order = order,
                             Product = product,
                         };

            return View(orders);
        }

        public IActionResult UserProfile()
        {
            var userName = User.Identity?.Name;
            var userData = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == userName);

            var Data = new DeliveryPersonRegistrationVM
            {
                Id = userData?.Id,
                UserName = userData?.DeliveryPersonName,
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
            var userData = _db.DeliveryPerson.FirstOrDefault(u => u.Id == id);

            var Data = new DeliveryPersonRegistrationVM
            {
                Id = id,
                UserName = userData?.DeliveryPersonName,
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
        public IActionResult UserProfileUpdate(DeliveryPersonRegistrationVM obj)
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

                var existingDeliveryPerson = _db.DeliveryPerson.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingDeliveryPerson != null)
                {
                    existingDeliveryPerson.DeliveryPersonName = obj.UserName;
                    existingDeliveryPerson.Email = obj.Email;
                    existingDeliveryPerson.FullName = obj.FullName;
                    existingDeliveryPerson.MobileNumber = obj.PhoneNumber;
                    existingDeliveryPerson.Password = obj.Password;
                }
                _db.Update(existingDeliveryPerson);

                _db.SaveChanges();
                TempData["success"] = "Profile Updated Successfully!";
                return RedirectToAction("UserProfile", "DeliveryPerson", new { area = "DeliveryPerson" });
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAllOrderForDelivery()
        {
            var username = User.Identity?.Name;
            var deliveryPersonID = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == username)?.DeliveryPersonID;

            var orders = from order in _db.Order
                         join product in _db.Product on order.ProductID equals product.ProductID
                         where order.DeliveryPersonID == deliveryPersonID && order.OrderStatus == "Shipped"
                         select new DeliveryPersonOrderVM
                         {
                             Order = order,
                             Product = product,
                         };

            return Json(new { data = orders.ToList() });
        }

        [HttpGet]

        public IActionResult GetAllPreviouslyDeliveredOrder()
        {
            var username = User.Identity?.Name;
            var deliveryPerson = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == username);

            var orders = from order in _db.Order
                         join product in _db.Product on order.ProductID equals product.ProductID
                         where order.DeliveryPersonID == deliveryPerson.DeliveryPersonID && order.OrderStatus == "Delivered"
                         select new DeliveryPersonOrderVM
                         {
                             Order = order,
                             Product = product,
                         };

            return Json(new { data = orders });
        }

        #endregion
    }
}
