using AIMobile.Helper;
using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

namespace AIMobile.Controllers
{
    public class ShopProductController : Controller
    {
        private readonly IShopProductService _shopProductService;
        private readonly IShopService _shopService;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ITypeServices _typeServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShopProductController(IShopProductService shopProductService,IShopService shopService,IProductService productService,IImageService imageService,ITypeServices typeServices, IWebHostEnvironment webHostEnvironment)
        {
            _shopProductService = shopProductService;
            _shopService = shopService;
            _productService = productService;
            _imageService = imageService;
            _typeServices = typeServices;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Entry()
        {
            try
            {
                IList<TypeViewModel> typeViewModel = _typeServices.ReteriveAll().Select(p => new TypeViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();


                return View(typeViewModel);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
        }
        [HttpGet]
        public IActionResult EntryShopProduct(string id)
        {
            try
            {
                ViewBag.Shop = _shopService.ReteriveAll().ToList();
                ViewBag.Product = _productService.ReteriveAll().Where(p => p.TypeId == id).ToList();
                ViewBag.Image = _imageService.ReteriveAll().ToList();
                TypeEntity typeEntity = _typeServices.GetById(id);
                TypeViewModel typeViewModel = new TypeViewModel()
                {
                    Id = typeEntity.Id,
                    Name = typeEntity.Name,
                };
                return View(typeViewModel);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Entry(ShopProductViewModel spv) {
            try
            {
                ShopProductEntity shopProductEntity = new ShopProductEntity()
                {

                    Id = Guid.NewGuid().ToString(),
                    ShopId = spv.ShopId,
                    ImageId = spv.ImageId,
                    ProductId = spv.ProductId,
                    Description = spv.Description,
                    StockCount = spv.StockCount,
                };
                _shopProductService.Entry(shopProductEntity);
                TempData["info"] = "Save Successfully the record to the system";

            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur when the data record to the system";
            }
            return RedirectToAction("Entry");
        }
        public IActionResult GetById(string id)
        {
            try
            {
                var shopProductEntity = _shopProductService.GetById("6b0dc9df-ecc5-4b0b-a2cd-a4b6eb9061b4");
                ShopProductViewModel shopProductViewModel = new ShopProductViewModel()
                {
                    Description = shopProductEntity.Description,
                    StockCount = shopProductEntity.StockCount,
                };
                return View(shopProductViewModel);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
        }

        public IActionResult List()
        {
            try
            {
                IList<ShopProductViewModel> shopProducts = _shopProductService.ReteriveAll().Select(sp => new ShopProductViewModel
                {
                    Id = sp.Id,
                    ShopId = sp.ShopId,
                    ProductId = sp.ProductId,
                    ImageId = sp.ImageId,
                    ShopName = _shopService.GetById(sp.ShopId)?.Name,
                    ProductName = _productService.GetById(sp.ProductId)?.Name,
                    ImageName = _imageService.GetById(sp.ImageId)?.ImageName,
                    Description = sp.Description,
                    StockCount = sp.StockCount,

                }).ToList();
                return View(shopProducts);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
            
        }


        public IActionResult Delete(string id)
        {
            try
            {
                _shopProductService.Delete(id);
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
            try
            {
                ShopProductViewModel shopProduct = new ShopProductViewModel();
                var SPDataModel = _shopProductService.GetById(id);
                if (SPDataModel != null)
                {
                    shopProduct.Id = SPDataModel.Id;
                    shopProduct.ShopId = SPDataModel.ShopId;
                    shopProduct.ProductId = SPDataModel.ProductId;
                    shopProduct.ImageId = SPDataModel.ImageId;
                    shopProduct.ShopName = _shopService.GetById(SPDataModel.ShopId).Name;
                    shopProduct.ProductName = _productService.GetById(SPDataModel.ProductId).Name;
                    shopProduct.ImageName = _imageService.GetById(SPDataModel.ImageId).ImageName;
                    shopProduct.Description = SPDataModel.Description;
                    shopProduct.StockCount = SPDataModel.StockCount;
                }
                ViewBag.FromShopId = _shopService.ReteriveAll().Select(s => new ShopViewModel { Id = s.Id, Name = s.Name }).ToList();
                ViewBag.FormProductId = _productService.ReteriveAll().Select(s => new ProductViewModel { Id = s.Id, Name = s.Name }).ToList();
                ViewBag.FormImageId = _imageService.ReteriveAll().Select(s => new ImageViewModel { Id = s.Id, ImageName = s.ImageName }).ToList();
                return View(shopProduct);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
            
        }


        [HttpPost]

        public IActionResult Update(ShopProductViewModel pvm)
        {
            try
            {
                ShopProductEntity shopProduct = new ShopProductEntity()
                {
                    Id = pvm.Id,
                    ShopId = pvm.ShopId,
                    ProductId = pvm.ProductId,
                    ImageId = pvm.ImageId,
                    Description = pvm.Description,
                    StockCount = pvm.StockCount,
                    UpdatedAt = DateTime.Now,
                };
                _shopProductService.Update(shopProduct);
                TempData["info"] = " Successfully update the record to the system";
            }
            catch (Exception e)
            {
                TempData["info"] = "Error when update the record to the system";
            }
            return RedirectToAction("List");
        }

        public IActionResult ShopProductReport()
        {
            ViewBag.Shop = _shopService.ReteriveAll().Select(s=> new ShopViewModel { Id=s.Id, Name=s.Name}).ToList();
            ViewBag.Product = _productService.ReteriveAll().Select(s=>new ProductViewModel { Id=s.Id,Name=s.Name}).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult ShopProductReport(string ShopId, string ProductId,int StockCount)
        {
            try
            {
                IList<ShopProductReportModel> ShopProductReports = _shopProductService.ReteriveAll().Where(w => w.ShopId == ShopId || w.ProductId == ProductId || w.StockCount == StockCount ).Select(s => new ShopProductReportModel
                {
                    ShopName = _shopService.GetById(s.ShopId).Name,
                    ProductName = _productService.GetById(s.ProductId).Name,
                    StockCount = s.StockCount
                }).ToList();
                if (ShopProductReports.Count > 0)
                {
                    var rdlcPath = Path.Combine(_webHostEnvironment.WebRootPath, "ReportFiles", "ShopProductReport.rdlc");
                    var fs = new FileStream(rdlcPath, FileMode.Open);
                    Stream reportDefination = fs;
                    LocalReport localReport = new LocalReport();
                    localReport.LoadReportDefinition(reportDefination);
                    localReport.DataSources.Add(new ReportDataSource("ShopProductReportDataSet", ShopProductReports));
                    byte[] pdffile = localReport.Render("pdf");
                    fs.Close();
                    return File(pdffile, "application/pdf");
                }
                else
                {
                    TempData["info"] = "There is no data";
                    return View();
                }
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur";
            }
            return View();
            
        }
    }
}
