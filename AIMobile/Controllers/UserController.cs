using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AIMobile.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Entry()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entry(UserViewModel uvm)
        {
            try
            {
                UserEntity user = new UserEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = uvm.Name,
                    PhoneNo = uvm.PhoneNo,
                    Email = uvm.Email,
                    Address = uvm.Address,
                };
                _userService.Entry(user);
                TempData["info"] = "Save Successfully the record to the system";
                return View();
            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur when the data record to the system";
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<UserViewModel> userViewModels = _userService.ReteriveAll().Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                PhoneNo = u.PhoneNo,
                Email = u.Email,
                Address = u.Address,
            }).ToList();
            return View(userViewModels);
        }

        
        public IActionResult Delete(string id)
        {
            try
            {
                _userService.Delete(id);
                TempData["Info"] = "Successfully delete the data";
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when delete the data";
            }
            return RedirectToAction("List");
        }

        

        public IActionResult Edit(string id)
        {
            UserViewModel user = new UserViewModel();
            var UserDataModel = _userService.GetById(id);
            if (UserDataModel != null)
            {
                user.Id = UserDataModel.Id;
                user.Name = UserDataModel.Name;
                user.PhoneNo = UserDataModel.PhoneNo;
                user.Email = UserDataModel.Email;
                user.Address = UserDataModel.Address;
            }
            return View(user);
        }

        
        [HttpPost]

        public IActionResult Update(UserViewModel uvm)
        {
            try
            {
                UserEntity user = new UserEntity()
                {
                    Id = uvm.Id,
                    Name = uvm.Name,
                    PhoneNo = uvm.PhoneNo,
                    Email = uvm.Email,
                    Address = uvm.Address,
                    UpdatedAt = DateTime.Now,
                };
                _userService.Update(user);
                TempData["info"] = " Successfully update the record to the system";
            }
            catch (Exception e)
            {
                TempData["info"] = "Error when update the record to the system";
            }
            return RedirectToAction("List");
        }
    }
}
