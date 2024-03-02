using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        public IActionResult Entry()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entry(ShopEntity svm)
        {
            try
            {
                var Shops = _shopService.ReteriveAll().Where(w => w.Name == svm.Name);
                if (Shops.Any())
                {
                    TempData["info"] = "Name already exists in the database";
                }
                else
                {
                    ShopEntity shop = new ShopEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = svm.Name,
                        Status = svm.Status,
                        Address = svm.Address,
                    };
                    _shopService.Entry(shop);
                    TempData["info"] = "Save Successfully the record to the system";

                }



            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur when the data record to the system";
            }
            return View();
        }

        public IActionResult List()
        {
            IList<ShopViewModel> shopViewModels = _shopService.ReteriveAll().Select(u => new ShopViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Status = u.Status,
                Address = u.Address,
            }).ToList();
            return View(shopViewModels);
        }


        public IActionResult Delete(string id)
        {
            try
            {
                _shopService.Delete(id);
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
            ShopViewModel shop = new ShopViewModel();
            var ShopDataModel = _shopService.GetById(id);
            if (ShopDataModel != null)
            {
                shop.Id = ShopDataModel.Id;
                shop.Name = ShopDataModel.Name;
                shop.Status = ShopDataModel.Status;
                shop.Address = ShopDataModel.Address;
            }
            return View(shop);
        }


        [HttpPost]

        public IActionResult Update(ShopViewModel uvm)
        {
            try
            {
                ShopEntity shop = new ShopEntity()
                {
                    Id = uvm.Id,
                    Name = uvm.Name,
                    Status = uvm.Status,
                    Address = uvm.Address,
                    UpdatedAt = DateTime.Now,
                };
                _shopService.Update(shop);
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
