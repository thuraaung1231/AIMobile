using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIMobileCus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageService _imageService;
        private readonly IShopProductService _shopProductService;
        private readonly IProductService _productService;
        private readonly ITypeServices _typeServices;

        public HomeController(ILogger<HomeController> logger, IImageService imageService, IShopProductService shopProductService, IProductService productService, ITypeServices typeServices)
        {
            _logger = logger;
            _imageService = imageService;
            _shopProductService = shopProductService;
            _productService = productService;
            _typeServices = typeServices;
        }

        public IActionResult Index()
        {
            IList<TypeViewModel> typeViewModels = _typeServices.ReteriveAll().Where(t => t.Name == "Phone").Select(p => new TypeViewModel
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}