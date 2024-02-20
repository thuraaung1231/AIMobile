using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

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
            var BrandEntity = new BrandEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name=bvm.Name,

            };
            return View();
        }
        public IActionResult list()
        {
            IList<BrandViewModels> brands =_brandService.ReteriveAll().Select(b=>new BrandViewModels { 
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
            return View();
        }    

    }
}
