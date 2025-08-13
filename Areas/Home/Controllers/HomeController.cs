using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CollegeProject.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login obj)
        {
            var supplierRole = _db.Supplier.FirstOrDefault(u => u.SupplierName == obj.UserName && u.Password == obj.Password)?.Role;
            var adminRole = _db.Admin.FirstOrDefault(u => u.AdminName == obj.UserName && u.Password == obj.Password)?.Role;
            var wareHouseManagerRole = _db.WareHouseManager.FirstOrDefault(u => u.WareHouseManagerName == obj.UserName && u.Password == obj.Password)?.Role;
            var deliveryPersonRole = _db.DeliveryPerson.FirstOrDefault(u => u.DeliveryPersonName == obj.UserName && u.Password == obj.Password)?.Role;
            var customer = _db.Customer.FirstOrDefault(u => u.CustomerName == obj.UserName);

            var userRole = adminRole;

            if (adminRole == null)
            {
                if (supplierRole != null)
                {
                    userRole = supplierRole;
                }
                else if (wareHouseManagerRole != null)
                {
                    userRole = wareHouseManagerRole;
                }
                else if (deliveryPersonRole != null)
                {
                    userRole = deliveryPersonRole;
                }
                else if(customer != null) 
                {
                    userRole = "Customer";
                }
            }


            if (userRole != null)
            { 
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, obj.UserName),
                    new Claim("Role", userRole)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = obj.RememberMe
                };

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                switch (userRole)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    case "DeliveryPerson":
                        return RedirectToAction("Index", "DeliveryPerson", new { area = "DeliveryPerson" });
                    case "WareHouseManager":
                        return RedirectToAction("Index", "WareHouseManager", new { area = "WareHouseManager" });
                    case "Supplier":
                        return RedirectToAction("Index", "Supplier", new { area = "Supplier" });
                    case "Customer":
                        return RedirectToAction("Index", "Customer", new { area = "Customer" });
                    default:
                        return View();
                }
            }
            else
            {
                TempData["error"] = "Invalid Login attempt!";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Home", new { area = "Home" });

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
