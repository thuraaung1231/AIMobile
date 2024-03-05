using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AIMobile.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(BrandViewModels bvm)
        {
            try
            {
                var BrandEntity = new BrandEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = bvm.Name,

                };
                _brandService.Entry(BrandEntity);
                TempData["Info"] = "Entry Success";

            }
            catch (Exception)
            {
                TempData["Info"] = "Entry Unsuccess";
                
            }
            return View();
        }
        public IActionResult list()
        {
            IList<BrandViewModels> brands =_brandService.ReteriveAll().Select(b=>new BrandViewModels
            { 
            Id=b.Id,
            Name=b.Name,
            
            }).ToList();
            return View(brands);
        }
        public IActionResult Delete( string Id) 
        {
            try
            {
                _brandService.Delete(Id);
                TempData["Info"] = "Successful Delete ";
            }
            catch (Exception)
            {
                TempData["Info"] = "Unsuccessful Delete ";
                throw;
            }
            return RedirectToAction("List");
        }
        [HttpGet]   
        
        public IActionResult Edit(string Id) { 
            var brandDataModel=_brandService.GetById(Id);
            BrandViewModels bvm = new BrandViewModels();
            BrandEntity brandEntity = new BrandEntity();
            if (brandDataModel!=null)
            {
                bvm.Id = brandDataModel.Id;
                bvm.Name = brandDataModel.Name;
            };

        return View(bvm);

        }
        [HttpPost]
        public IActionResult Edit(BrandViewModels bvm) {
            try
            {
                BrandEntity brandEntity = new BrandEntity()
                {
                    Id = bvm.Id,
                    Name = bvm.Name,
                };
                _brandService.Update(brandEntity);
                TempData["Info"] = "Update Successful";
            }
            catch (Exception)
            {
                TempData["Info"] = "Update Unsuccessful";
                throw;
            }
        return RedirectToAction("List");
        
        
        }

    }
}
