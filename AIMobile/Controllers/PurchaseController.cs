using AIMobile.Helper;
using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;

using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
namespace AIMobile.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShopProductService _shopProductService;

        public PurchaseController(IShopService shopService,IPurchaseService purchaseService, IProductService productService,IWebHostEnvironment webHostEnvironment,IShopProductService shopProductService)

        private readonly IPurchaseService _purchaseService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShopProductService _shopProductService;
        private readonly IPaymentTypeService _paymentTypeService;


        public PurchaseController(IShopService shopService,IProductService productService,IPurchaseService purchaseService,IWebHostEnvironment webHostEnvironment,IShopProductService shopProductService)


        public PurchaseController(IShopService shopService, IProductService productService, IPurchaseService purchaseService, IWebHostEnvironment webHostEnvironment, IShopProductService shopProductService, IPaymentTypeService paymentTypeService)

        {
            _shopService = shopService;
            _purchaseService = purchaseService;
            _productService = productService;

            _webHostEnvironment = webHostEnvironment;
            _shopProductService = shopProductService;
        }
      

            _purchaseService = purchaseService;
            _webHostEnvironment = webHostEnvironment;
            _shopProductService = shopProductService;
            _paymentTypeService = paymentTypeService;
        }

        public IActionResult List()
        {
            try
            {
                IList<PurchaseViewModel> purchaseViewModels = _purchaseService.ReteriveAll().Select(u => new PurchaseViewModel
                {
                    Id = u.Id,
                    ShopProductId = u.ShopProductId,
                    CustomerId = u.CustomerId,
                    PurchaseDateTime = u.PurchaseDateTime,
                    PaymentTypeId = u.PaymentTypeId,
                    TotalPrice = u.TotalPrice,
                    ScreenShot = u.ScreenShot,
                    DeliId = u.DeliId,
                    TransactionId = u.TransactionId,
                }).ToList();
                return View(purchaseViewModels);
            }
            catch (Exception)
            {
                TempData["info"] = "Error occur ";
            }
            return View();
        }


        public IActionResult Delete(string id)
        {
            try
            {
                _purchaseService.Delete(id);
                TempData["Info"] = "Successfully delete the data";
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when delete the data";
            }
            return RedirectToAction("List");
        }
        public IActionResult PurchaseReport()
        {
            ViewBag.Shop = _shopService.ReteriveAll().Select(s => new ShopViewModel { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.Product = _productService.ReteriveAll().Select(s => new ProductViewModel { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.ShopProduct = _shopProductService.ReteriveAll().Select(s => new ShopProductViewModel { Id = s.Id, ShopId = s.ShopId }).ToList();
            return View();
        }

        [HttpPost]
       
        public IActionResult PurchaseReport(string shopId,DateTime fromDate,DateTime toDate)
        {
            IList<PurchaseReportModel> purchaseReportModels = new List<PurchaseReportModel>();
            IList<ShopProductViewModel> shopProducts = _shopProductService.ReteriveAll().Where(w => w.ShopId == shopId).Select(s => new ShopProductViewModel
            {
                Id = s.Id,
                ShopId = s.ShopId,
                ShopName = _shopService.GetById(shopId).Name,
                ProductName = _productService.GetById(s.ProductId).Name,
            }).ToList();
            foreach (var shopProduct in shopProducts)
            { 
                IList<PurchaseEntity> purchaseReports = _purchaseService.GetByShopProductAndDate(shopProduct.Id,fromDate,toDate);
                if(purchaseReports.Count>0)
                {
                    foreach(var purchaseReport in purchaseReports)
                    {
                        PurchaseReportModel purchaseReportModel = new PurchaseReportModel()
                        {
                            Id = purchaseReport.Id,
                            TotalPrice = purchaseReport.TotalPrice,
                            PurchaseDate = purchaseReport.PurchaseDateTime,
                            CustomerId = purchaseReport.CustomerId,
                            ShopProductId = purchaseReport.ShopProductId,
                            PaymentTypeId = purchaseReport.PaymentTypeId,
                            ProductName = shopProduct.ProductName,
                            ShopName = shopProduct.ShopName,

                        };
                        purchaseReportModels.Add(purchaseReportModel);
                    }
                   
                }
                else
                {
                    Console.WriteLine("Error Occur");
                }
          
            }

            if (purchaseReportModels.Count > 0)
                {
                    var rdlcPath = Path.Combine(_webHostEnvironment.WebRootPath, "ReportFiles", "PurchaseReport.rdlc");
                    var fs = new FileStream(rdlcPath, FileMode.Open);
                    Stream reportDefination = fs;
                    LocalReport localReport = new LocalReport();
                    localReport.LoadReportDefinition(reportDefination);
                    localReport.DataSources.Add(new ReportDataSource("PurchaseDataSet", purchaseReportModels));
                    byte[] pdffile = localReport.Render("pdf");
                    fs.Close();
                    return File(pdffile, "application/pdf");
                }
                else
                {
                    TempData["info"] = "There is no data";

                }     
            return View();
        }

    }
}
