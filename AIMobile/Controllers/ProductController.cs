using AIMobile.Helper;
using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

namespace AIMobile.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IBrandService brandService, IProductService productService,ITypeServices typeServices,IWebHostEnvironment webHostEnvironment)
        {
            _brandService = brandService;
            _productService = productService;
            _typeServices = typeServices;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            ViewBag.FromBrandId = _brandService.ReteriveAll().Select(s=>new BrandViewModels {Id=s.Id,Name=s.Name}).ToList();
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
                BrandName =_brandService.GetById(b.BrandId)?.Name,
                BrandId = b.BrandId,
                TypeName =_typeServices.GetById(b.TypeId)?.Name,
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
            //Test Branch

        }

        public IActionResult ProductDetailReport()
        {
            ViewBag.Type = _typeServices.ReteriveAll().Select(s => new TypeViewModel { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.Brand = _brandService.ReteriveAll().Select(s => new BrandViewModels { Id = s.Id, Name = s.Name }).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult ProductDetailReport(string TypeId, string BrandId,decimal UnitPrice)
        {
            IList<ProductDetailReport> products = _productService.ReteriveAll().Where(w =>
            w.TypeId==TypeId|| w.BrandId==BrandId|| w.UnitPrice==UnitPrice).Select(s => new ProductDetailReport
            {
                TypeName=_typeServices.GetById(TypeId).Name,
                Name=s.Name,
              
                UnitPrice = s.UnitPrice,

            }).ToList();
            if (products.Count > 0)
            {
                var rdlcPath = Path.Combine(_webHostEnvironment.WebRootPath, "ReportFile", "ProductDetailReport.rdlc");
                var fs = new FileStream(rdlcPath, FileMode.Open);
                Stream reportDefination = fs;
                LocalReport localReport = new LocalReport();
                localReport.LoadReportDefinition(reportDefination);
                localReport.DataSources.Add(new ReportDataSource("ProductDetailReport", products));
                byte[] pdffile = localReport.Render("pdf");
                fs.Close();
                return File(pdffile, "application/pdf");
            }
            else
            {
                TempData["Info"] = "There is no data";
                return View();
            }

        }

    }
}
