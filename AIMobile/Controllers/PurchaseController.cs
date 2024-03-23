using AIMobile.Helper;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IProductService _productService;

        public PurchaseController(IShopService shopService,IProductService productService)
        {
            _shopService = shopService;
            _productService = productService;
        }
        public IActionResult PurchaseReport()
        {
            ViewBag.Shop = _shopService.ReteriveAll().Select(s => new ShopViewModel { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.Product = _productService.ReteriveAll().Select(s => new ProductViewModel { Id = s.Id, Name = s.Name }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PurchaseReport(string ShopId,string ProductId,DateTime PurchaseDateTime)
        {
            return View();
        }
    }
}
