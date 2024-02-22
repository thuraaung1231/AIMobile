using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;

        public ProductController(IBrandService brandService, IProductService productService,ITypeServices typeServices)
        {
            _brandService = brandService;
            _productService = productService;
            _typeServices = typeServices;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            ViewBag.FromBrandId = _brandService.ReteriveAll().Select(s=>new BrandViewModels {Id=s.Id,Name=s.Name }).ToList();
            ViewBag.FromTypeId = _typeServices.ReteriveAll().Select(s => new TypeViewModel { Id = s.Id, Name = s.Name }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Entry(ProductViewModel pvm)
        {
            try
            {
                var ProductEntity = new ProductEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = pvm.Name,
                    UnitPrice = pvm.UnitPrice,
                    BrandId=pvm.BrandId,
                    TypeId = pvm.TypeId,

                };
                _productService.Entry(ProductEntity);
                TempData["Info"] = "Entry Success";

            }
            catch (Exception)
            {
                TempData["Info"] = "Entry Unsuccess";

            }
            return RedirectToAction("List");
        }
        public IActionResult list()
        {
            IList<ProductViewModel> product = _productService.ReteriveAll().Select(b => new ProductViewModel
            {
                Id = b.Id,
                Name = b.Name,
                UnitPrice = b.UnitPrice,
                BrandName =_brandService.GetById(b.BrandId).Name,
                BrandId = b.BrandId,
                TypeName =_typeServices.GetById(b.TypeId).Name,
                TypeId=b.TypeId,

            }).ToList();
            return View(product);
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _productService.Delete(Id);
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

        public IActionResult Edit(string Id)
        {
            var ProductDataModel = _productService.GetById(Id);
            ProductViewModel pvm = new ProductViewModel();
         
            if (ProductDataModel != null)
            {
                pvm.Id = ProductDataModel.Id;
                pvm.Name = ProductDataModel.Name;
                pvm.UnitPrice = ProductDataModel.UnitPrice; 
                pvm.BrandId = ProductDataModel.BrandId;
                pvm.BrandName = _brandService.GetById(ProductDataModel.BrandId).Name;
                pvm.TypeId = ProductDataModel.TypeId;
                pvm.TypeName = _typeServices.GetById(ProductDataModel.TypeId).Name;
            };
            ViewBag.FromBrandId = _brandService.ReteriveAll().Select(s => new BrandViewModels { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.FromTypeId = _typeServices.ReteriveAll().Select(s => new TypeViewModel { Id = s.Id, Name = s.Name }).ToList();


            return View(pvm);

        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel pvm)
        {
            try
            {
                ProductEntity productEntity = new ProductEntity()
                {
                    Id = pvm.Id,
                    Name = pvm.Name,
                    UnitPrice = pvm.UnitPrice,
                    BrandId = pvm.BrandId,
                    TypeId = pvm.TypeId,
                };
                _productService.Update(productEntity);
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
