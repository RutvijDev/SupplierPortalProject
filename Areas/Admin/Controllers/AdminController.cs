using CollegeProject.Data;
using CollegeProject.Models;
using CollegeProject.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "MustBelongToAdmin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()
        {
            List<User> UserList = _db.User.ToList();
            return View(UserList);
        }

        [HttpPost]
        public IActionResult UserRegistration(string Role)
        {
            if (Role == "Admin")
            {
                return RedirectToAction("AdminRegistration");
            }
            else if(Role == "Supplier")
            {
                return RedirectToAction("SupplierRegistration");
            }
            else if(Role == "WareHouseManager")
            {
                return RedirectToAction("WareHouseManagerRegistration");
            }
            else if(Role == "DeliveryPerson")
            {
                return RedirectToAction("DeliveryPersonRegistration");
            }
            else
            {
                return RedirectToAction("UserList");
            }
        }

        public IActionResult AdminRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminRegistration(AdminRegistrationVM obj)
        {
            var userName = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.UserName;
            if(userName != null)
            {
                TempData["Error"] = "Username already exists! Please choose a different username!";
                return View(obj);
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = obj.Id,
                    UserName = obj.UserName,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Password = obj.Password,
                    Role = obj.Role,
                };

                _db.User.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User Created Successfully!";

                string nextAdminID = GenerateNextAdminID();

                var admin = new CollegeProject.Models.Admin
                {
                    AdminID = nextAdminID,
                    AdminName = obj.UserName,
                    FullName = obj.FullName,
                    Id = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.Id,
                    MobileNumber = obj.PhoneNumber,
                    Email = obj.Email,
                    Password = obj.Password,
                    Role = obj.Role,
                };
                _db.Admin.Add(admin);
                _db.SaveChanges();

                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        private string GenerateNextAdminID()
        {
            string lastAdminID = _db.Admin.OrderByDescending(a => a.AdminID).Select(a => a.AdminID).FirstOrDefault();

            string nextAdminID;
            if (lastAdminID == null)
            {
                nextAdminID = "A001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastAdminID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextAdminID = $"A{nextNumericPart:D3}";
            }

            return nextAdminID;
        }

        public IActionResult SupplierRegistration()
        {
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View();
        }

        [HttpPost]
        public IActionResult SupplierRegistration(SupplierRegistrationVM obj)
        {
            var userName = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.UserName;
            if (userName != null)
            {
                TempData["Error"] = "Username already exists! Please choose a different username!";
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = obj.Id,
                    UserName = obj.UserName,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Password = obj.Password,
                    Role = obj.Role,
                };

                _db.User.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User Created Successfully!";

                string nextSupplierID = GenerateNextSupplierID();

                var supplier = new CollegeProject.Models.Supplier
                {
                    SupplierID = nextSupplierID,
                    SupplierName = obj.UserName,
                    FullName = obj.FullName,
                    Id = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.Id,
                    MobileNumber = obj.PhoneNumber,
                    Email = obj.Email,
                    Role = obj.Role,
                    Password = obj.Password,
                    WareHouseID = obj.WareHouseID,
                };
                _db.Supplier.Add(supplier);
                _db.SaveChanges();

                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        private string GenerateNextSupplierID()
        {
            string lastSupplierID = _db.Supplier.OrderByDescending(a => a.SupplierID).Select(a => a.SupplierID).FirstOrDefault();

            string nextSupplierID;
            if (lastSupplierID == null)
            {
                nextSupplierID = "S001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastSupplierID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextSupplierID = $"S{nextNumericPart:D3}";
            }

            return nextSupplierID;
        }

        public IActionResult WareHouseManagerRegistration()
        {
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View();
        }

        [HttpPost]
        public IActionResult WareHouseManagerRegistration(WareHouseManagerRegistrationVM obj)
        {
            var userName = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.UserName;
            if (userName != null)
            {
                TempData["Error"] = "Username already exists! Please choose a different username!";
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = obj.Id,
                    UserName = obj.UserName,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Password = obj.Password,
                    Role = obj.Role,
                };

                _db.User.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User Created Successfully!";

                string nextWareHouseManagerID = GenerateNextWareHouseManagerID();

                var wareHouseManager = new CollegeProject.Models.WareHouseManager
                {
                    WareHouseManagerID = nextWareHouseManagerID,
                    WareHouseManagerName = obj.UserName,
                    FullName = obj.FullName,
                    Id = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.Id,
                    MobileNumber = obj.PhoneNumber,
                    Email = obj.Email,
                    Password = obj.Password,
                    WareHouseID = obj.WareHouseID,
                    Role = obj.Role,
                };
                _db.WareHouseManager.Add(wareHouseManager);
                _db.SaveChanges();

                return RedirectToAction("UserList", "Admin");
            }
                return View();
        }

        private string GenerateNextWareHouseManagerID()
        {
            string lastWareHouseManagerID = _db.WareHouseManager.OrderByDescending(a => a.WareHouseManagerID).Select(a => a.WareHouseManagerID).FirstOrDefault();

            string nextWareHouseManagerID;
            if (lastWareHouseManagerID == null)
            {
                nextWareHouseManagerID = "WM001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastWareHouseManagerID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextWareHouseManagerID = $"WM{nextNumericPart:D3}";
            }

            return nextWareHouseManagerID;
        }

        public IActionResult DeliveryPersonRegistration()
        {
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View();
        }

        [HttpPost]
        public IActionResult DeliveryPersonRegistration(DeliveryPersonRegistrationVM obj)
        {
            var userName = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.UserName;
            if (userName != null)
            {
                TempData["Error"] = "Username already exists! Please choose a different username!";
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = obj.Id,
                    UserName = obj.UserName,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Password = obj.Password,
                    Role = obj.Role,
                };

                _db.User.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User Created Successfully!";

                string nextDeliveryPersonID = GenerateNextDeliveryPersonID();

                var deliveryPerson = new CollegeProject.Models.DeliveryPerson
                {
                    DeliveryPersonID = nextDeliveryPersonID,
                    DeliveryPersonName = obj.UserName,
                    FullName = obj.FullName,
                    Id = _db.User.FirstOrDefault(u => u.UserName == obj.UserName)?.Id,
                    MobileNumber = obj.PhoneNumber,
                    Email = obj.Email,
                    Password = obj.Password,
                    WareHouseID = obj.WareHouseID,
                    Role = obj.Role,
                };
                _db.DeliveryPerson.Add(deliveryPerson);
                _db.SaveChanges();

                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        private string GenerateNextDeliveryPersonID()
        {
            string lastDeliveryPersonID = _db.DeliveryPerson.OrderByDescending(a => a.DeliveryPersonID).Select(a => a.DeliveryPersonID).FirstOrDefault();

            string nextDeliveryPersonID;
            if (lastDeliveryPersonID == null)
            {
                nextDeliveryPersonID = "D001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastDeliveryPersonID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextDeliveryPersonID = $"D{nextNumericPart:D3}";
            }

            return nextDeliveryPersonID;
        }

        public IActionResult EditUser(int id)
        {
            var Role = _db.User.FirstOrDefault(u => u.Id == id)?.Role;
            if (Role == "Admin")
            {
                return RedirectToAction("AdminUserEdit", new {id = id});
            }
            else if (Role == "Supplier")
            {
                return RedirectToAction("SupplierUserEdit", new { id = id });
            }
            else if (Role == "WareHouseManager")
            {
                return RedirectToAction("WareHouseManagerUserEdit", new { id = id });
            }
            else if (Role == "DeliveryPerson")
            {
                return RedirectToAction("DeliveryPersonUserEdit", new { id = id });
            }
            else
            {
                return RedirectToAction("UserList");
            }
        }

        public IActionResult AdminUserEdit(int id)
        {
            var userData = _db.Admin.FirstOrDefault(u => u.Id == id);
            var Data = new AdminRegistrationVM
            {
                Id = id,
                UserName = userData?.AdminName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };
            return View(Data);
        }

        [HttpPost]
        public IActionResult AdminUserEdit(AdminRegistrationVM obj)
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
                if(existingUser != null)
                {
                    existingUser.FullName = obj.FullName;
                    existingUser.UserName = obj.UserName;
                    existingUser.Email = obj.Email;
                    existingUser.PhoneNumber = obj.PhoneNumber;
                    existingUser.Password = obj.Password;
                }

                _db.User.Update(existingUser);

                var existingAdmin = _db.Admin.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingAdmin != null)
                {
                    existingAdmin.AdminName = obj.UserName;
                    existingAdmin.Email = obj.Email;
                    existingAdmin.FullName = obj.FullName;
                    existingAdmin.MobileNumber = obj.PhoneNumber;
                    existingAdmin.Password = obj.Password;
                }
                _db.Update(existingAdmin);

                _db.SaveChanges();
                TempData["success"] = "User Updated Successfully!";
                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        public IActionResult SupplierUserEdit(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        [HttpPost]
        public IActionResult SupplierUserEdit(SupplierRegistrationVM obj)
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
                TempData["success"] = "User Updated Successfully!";
                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        public IActionResult WareHouseManagerUserEdit(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        [HttpPost]
        public IActionResult WareHouseManagerUserEdit(WareHouseManagerRegistrationVM obj)
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
                TempData["success"] = "User Updated Successfully!";
                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        public IActionResult DeliveryPersonUserEdit(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        [HttpPost]
        public IActionResult DeliveryPersonUserEdit(DeliveryPersonRegistrationVM obj)
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
                TempData["success"] = "User Updated Successfully!";
                return RedirectToAction("UserList", "Admin");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var userRole = _db.User.FirstOrDefault(u => u.Id == id)?.Role;

            if (userRole == "Admin")
            {
                var userData = _db.Admin.FirstOrDefault(u => u.Id == id);
                return RedirectToAction("AdminDelete", new {id = id});
            }
            else if(userRole == "Supplier")
            {
                var userData = _db.Supplier.FirstOrDefault(u => u.Id == id);
                return RedirectToAction("SupplierDelete", new { id = id });
            }
            else if(userRole == "WareHouseManager")
            {
                var userData = _db.WareHouseManager.FirstOrDefault(u =>u.Id == id);
                return RedirectToAction("WareHouseManagerDelete", new { id = id });
            }
            else if(userRole == "DeliveryPerson")
            {
                var userData = _db.DeliveryPerson.FirstOrDefault(u => u.Id == id);
                return RedirectToAction("DeliveryPersonDelete", new { id = id });
            }
            return NotFound();
        }

        public IActionResult AdminDelete(int id)
        {       
            var userData = _db.Admin.FirstOrDefault(u => u.Id == id);
            var Data = new AdminRegistrationVM
            {
                Id = id,
                UserName = userData?.AdminName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };
            return View(Data);
        }

        public IActionResult SupplierDelete(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        public IActionResult WareHouseManagerDelete(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        public IActionResult DeliveryPersonDelete(int id)
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
            IEnumerable<SelectListItem> WareHouseIDList = _db.WareHouse.Select(u => new SelectListItem
            {
                Text = u.WareHouseName,
                Value = u.WareHouseID
            });
            ViewBag.WareHouse = WareHouseIDList;
            return View(Data);
        }

        [HttpPost("Admin/Admin/AdminDelete/{id}")]
        [HttpPost("Admin/Admin/SupplierDelete/{id}")]
        [HttpPost("Admin/Admin/WareHouseManagerDelete/{id}")]
        [HttpPost("Admin/Admin/DeliveryPersonDelete/{id}")]
        public IActionResult ConfirmDelete(int? id)
        {
            var userData = _db.User.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (userData?.Role == "Supplier")
            {
                var supplierData = _db.Supplier.AsNoTracking().FirstOrDefault(u => u.Id == id);
                _db.Supplier.Remove(supplierData);
                _db.SaveChanges();
            }
            else if (userData?.Role == "Admin")
            {
                var adminData = _db.Admin.AsNoTracking().FirstOrDefault(u => u.Id == id);
                _db.Admin.Remove(adminData);
                _db.SaveChanges();
            }
            else if (userData?.Role == "WareHouseManager")
            {
                var wareHouseManager = _db.WareHouseManager.AsNoTracking().FirstOrDefault(u => u.Id == id);
                _db.WareHouseManager.Remove(wareHouseManager);
                _db.SaveChanges();
            }
            else if (userData?.Role == "DeliveryPerson")
            {
                var deliveryPersonData = _db.DeliveryPerson.AsNoTracking().FirstOrDefault(u => u.Id == id);
                _db.DeliveryPerson.Remove(deliveryPersonData);
                _db.SaveChanges();
            }
            else
            {
                return RedirectToAction("UserList");
            }

            _db.User.Remove(userData);
            _db.SaveChanges();
            TempData["success"] = "User Deleted Successfully!";
            return RedirectToAction("UserList", "Admin");
        }

        public IActionResult WareHouseList()
        {
            var wareHouseList = _db.WareHouse.ToList();
            return View(wareHouseList);
        }

        public IActionResult AddWareHouse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWareHouse(WareHouseVM wareHouse)
        {
            if (ModelState.IsValid)
            {
                var wareHouseID = GenerateNextWareHouseID();

                var wareHouseDetail = new WareHouse
                {
                    WareHouseID = wareHouseID,
                    WareHouseName = wareHouse.WareHouseName,
                    WareHouseAddress = wareHouse.WareHouseAddress
                };
                _db.Add(wareHouseDetail);
                _db.SaveChanges();
                TempData["success"] = "WareHouse Added Successfully!";
                return RedirectToAction("WareHouseList");
            }
            return View(wareHouse);
        }

        private string GenerateNextWareHouseID()
        {
            string lastWareHouseID = _db.WareHouse.OrderByDescending(a => a.WareHouseID).Select(a => a.WareHouseID).FirstOrDefault();

            string nextWareHouseID;
            if (lastWareHouseID == null)
            {
                nextWareHouseID = "W001";
            }
            else
            {
                int lastNumericPart = int.Parse(lastWareHouseID.Substring(2));
                int nextNumericPart = lastNumericPart + 1;
                nextWareHouseID = $"W{nextNumericPart:D3}";
            }

            return nextWareHouseID;
        }

        public IActionResult UpdateWareHouse(string ID)
        {
            var wareHouse = _db.WareHouse.FirstOrDefault(u => u.WareHouseID == ID);

            var wareHouseDetail = new WareHouseVM
            {
                WareHouseID = ID,
                WareHouseName = wareHouse?.WareHouseName,
                WareHouseAddress = wareHouse?.WareHouseAddress
            };
            return View(wareHouseDetail);
        }

        [HttpPost]
        public IActionResult UpdateWareHouse(WareHouseVM wareHouse)
        {
            if (ModelState.IsValid)
            {
                var existingWareHouse = _db.WareHouse.FirstOrDefault(u => u.WareHouseID == wareHouse.WareHouseID);
                if (existingWareHouse != null)
                {
                    existingWareHouse.WareHouseName = wareHouse.WareHouseName;
                    existingWareHouse.WareHouseAddress = wareHouse.WareHouseAddress;
                }

                _db.Update(existingWareHouse);
                _db.SaveChanges();
                TempData["success"] = "WareHouse Updated Successfully!";
                return RedirectToAction("WareHouseList");
            }
            return View(wareHouse);
        }

        public IActionResult DeleteWareHouse(string ID)
        {
            var wareHouse = _db.WareHouse.FirstOrDefault(u => u.WareHouseID == ID);

            var wareHouseDetail = new WareHouseVM
            {
                WareHouseID = ID,
                WareHouseName = wareHouse?.WareHouseName,
                WareHouseAddress = wareHouse?.WareHouseAddress
            };
            return View(wareHouseDetail);
        }

        [HttpPost, ActionName("DeleteWareHouse")]
        public IActionResult DeleteConfirmWareHouse(string ID)
        {
            var wareHouse = _db.WareHouse.FirstOrDefault(u => u.WareHouseID == ID);

            _db.Remove(wareHouse);
            _db.SaveChanges();
            TempData["success"] = "WareHouse Deleted Successfully!";
            return RedirectToAction("WareHouseList");
        }

        public IActionResult UserProfile()
        {
            var userName = User.Identity?.Name;
            var userData = _db.Admin.FirstOrDefault(u => u.AdminName == userName);

            var Data = new AdminRegistrationVM
            {
                Id = userData?.Id,
                UserName = userData?.AdminName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };

            return View(Data);
        }

        public IActionResult UserProfileUpdate(int id)
        {
            var userData = _db.Admin.FirstOrDefault(u => u.Id == id);

            var Data = new AdminRegistrationVM
            {
                Id = userData?.Id,
                UserName = userData?.AdminName,
                FullName = userData?.FullName,
                Email = userData?.Email,
                PhoneNumber = userData?.MobileNumber,
                Role = userData?.Role,
                Password = userData?.Password,
                ConfirmPassword = userData?.Password
            };
            return View(Data);
        }

        [HttpPost]
        public IActionResult UserProfileUpdate(AdminRegistrationVM obj)
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

                var existingAdmin = _db.Admin.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);
                if (existingAdmin != null)
                {
                    existingAdmin.AdminName = obj.UserName;
                    existingAdmin.Email = obj.Email;
                    existingAdmin.FullName = obj.FullName;
                    existingAdmin.MobileNumber = obj.PhoneNumber;
                    existingAdmin.Password = obj.Password;
                }
                _db.Update(existingAdmin);

                _db.SaveChanges();
                TempData["success"] = "Profile Updated Successfully!";
                return RedirectToAction("UserProfile", "Admin", new { area = "Admin" });
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAllUserList()
        {
            List<User> UserList = _db.User.ToList();
            return Json(new { data = UserList } );
        }

        [HttpGet]
        public IActionResult GetAllWareHouseList()
        {
            var wareHouseList = _db.WareHouse.ToList();
            return Json(new { data = wareHouseList });
        }

        #endregion
    }
}
