using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobile.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class AdminRegisterController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminRegisterController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(string EmailAddress,string Password)
        {
            AdminRegisterEntity adminRegisterEntity = _adminService.RetrieveByEmailAndPassword(EmailAddress,Password);
            if(adminRegisterEntity != null) {
                SessionHelper.SetDataToSession(HttpContext.Session, "EmailAddress", EmailAddress);
                return RedirectToAction("List", "Brand");
            }
            else
            {
                TempData["info"] = "Failed to Access Login";
                return RedirectToAction("index", "home");
            }
            
            
        }

        [HttpPost]
        public IActionResult Register(AdminRegisterViewModel adminRegister)
        {
            bool notSamePassword = false;
            if (adminRegister.Password != adminRegister.ConfirmPassword)
            {
                notSamePassword = true;

            }
            else
            {
                notSamePassword = false;
            }
            AdminRegisterEntity Admin = _adminService.GetByEmail(adminRegister.EmailAddress);
            if(Admin != null) {
                TempData["Info"] = "Your Email Address already registred!";
                return RedirectToAction("register", "adminregister");
            }
            else if(Admin==null && notSamePassword)
            {
                TempData["Info"] = "Password and Confirm confirm password are different";
                return RedirectToAction("register", "adminregister");
            }
            else if(Admin==null && !notSamePassword)
            {
                AdminRegisterEntity adminRegisterEntity = new AdminRegisterEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailAddress = adminRegister.EmailAddress,
                    Password = adminRegister.Password,
                };

                // Add new admin
                _adminService.Entry(adminRegisterEntity);

                TempData["Info"] = "Successfully Registered!";
            }
            //IList<AdminRegisterViewModel> AdminViews=_adminService.RetrieveAll().Select(a=>new AdminRegisterViewModel
            //{
            //    Id = a.Id,
            //    EmailAddress=a.EmailAddress,
            //    Password=a.Password,
            //}).ToList();
            //bool hasBeenRegister=false;
            //foreach(var adminView in AdminViews) { 
            //    if(adminView.EmailAddress == adminRegister.EmailAddress)
            //    {
            //        hasBeenRegister = true;
            //        break;
            //    }
               
            //}
            
            //if (hasBeenRegister)
            //{
            //    TempData["Info"] = "Your Email Address already registred!";
            //}
            //else
            //{
            //    try
            //    {
            //        // Create new AdminRegisterEntity
            //        AdminRegisterEntity adminRegisterEntity = new AdminRegisterEntity()
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            EmailAddress = adminRegister.EmailAddress,
            //            Password = adminRegister.Password, 
            //        };

            //        // Add new admin
            //        _adminService.Entry(adminRegisterEntity);

            //        TempData["Info"] = "Successfully Registered!";
            //    }
            //    catch (Exception ex)
            //    {
                    
            //        ViewBag.Message = "An error occurred while processing your request. Please try again later.";
            //        // Log the exception for further analysis
                    
            //    }
            //}
            return RedirectToAction("Index","Home");
        }
        public IActionResult Logout()
        {
            SessionHelper.ClearSession(HttpContext.Session);
            return RedirectToAction("Index","Home");
        }
    }
}
