using AIMobile.Helper;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Hosting;
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
        {
            _shopService = shopService;
            _purchaseService = purchaseService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _shopProductService = shopProductService;
        }
      
    }
}
