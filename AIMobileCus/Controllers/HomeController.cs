using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models;
using AIMobileCus.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
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
        private readonly IShopService _shopService;

        public HomeController(ILogger<HomeController> logger, IImageService imageService, IShopProductService shopProductService, IProductService productService, ITypeServices typeServices, IShopService shopService)
        {
            _logger = logger;
            _imageService = imageService;
            _shopProductService = shopProductService;
            _productService = productService;
            _typeServices = typeServices;
            _shopService = shopService;
        }

        public IActionResult Index()
        {

            ViewBag.product = _productService.ReteriveAll().Select(s => new ProductViewModel { Name = s.Name }).ToList();
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
        [HttpPost]
        public JsonResult Search(string searchName) {
            /* IList<ShopProductViewModel> spvm=new List<ShopProductViewModel>();*/



            /*  foreach (var product in pvm)
              {
                  var shopProductViewModel = _shopProductService.GetById(product.Id);
                  ShopProductViewModel shopProduct = new ShopProductViewModel
                  {
                      Id = shopProductViewModel.Id,
                      Image_Id = shopProductViewModel.Image_Id,
                      Shop_Id = shopProductViewModel.Shop_Id,
                      Product_Id = shopProductViewModel.Product_Id,

                      ShopName = shopProductViewModel.Shop_Id,

                      Description = shopProductViewModel.Description,
                  };
                  spvm.Add(shopProduct);
              }*/
            var typeId = _typeServices.GetByName(searchName).Id;



            return Json(typeId);
        }
        public IActionResult SearchListResult(string SearchListData) {


            return View();
        }
        [HttpGet]
        public IActionResult SearchList(string Typedata)
        { var typeid = JsonConvert.DeserializeObject<string>(Typedata);
            var pvm = _productService.ReteriveAll().Where(w => w.TypeId == typeid).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
            var spvm = _shopProductService.ReteriveAll().Select(s => new ShopProductViewModel
            {
                Id = s.Id,
                Shop_Id = s.Shop_Id,
                Product_Id = s.Product_Id,
                Image_Id = s.Image_Id,
                ShopName = _shopService.GetById(s.Shop_Id).Name,
                ProductName = _productService.GetById(s.Product_Id).Name,
                ImageName = _imageService.GetById(s.Image_Id).ImageName,
                StockCount = s.StockCount,
                Description = s.Description,

            }).ToList();
            IList<ShopProductResultViewModel> sprvm = (from t in pvm
                                                       join s in spvm on t.Id equals s.Product_Id
                                                       select new ShopProductResultViewModel
                                                       {
                                                           Id = s.Id,
                                                           Shop_Id = s.Shop_Id,
                                                           Product_Id = s.Product_Id,
                                                           Image_Id = s.Image_Id,
                                                           ShopName = _shopService.GetById(s.Shop_Id).Name,
                                                           ProductName = _productService.GetById(s.Product_Id).Name,
                                                           ImageName = _imageService.GetById(s.Image_Id).FrontImageUrl,
                                                           StockCount = s.StockCount,
                                                           Description = s.Description,
                                                           Unitprice = _productService.GetById(s.Product_Id).UnitPrice,

                                                       }).ToList();

            return View(sprvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Filter(string Id) 
        {
            var productList=_shopProductService.ReteriveAll().Where(w=>w.Product_Id==Id).Select(s=> new ShopProductResultViewModel {

                Id = s.Id,
                Shop_Id = s.Shop_Id,
                Product_Id = s.Product_Id,
                Image_Id = s.Image_Id,
                ShopName = _shopService.GetById(s.Shop_Id).Name,
                ProductName = _productService.GetById(s.Product_Id).Name,
                ImageName = _imageService.GetById(s.Image_Id).FrontImageUrl,
                StockCount = s.StockCount,
                Description = s.Description,
                Unitprice = _productService.GetById(s.Product_Id).UnitPrice,

            }).ToList();
            
            
            return View("SearchList",productList);
        }
        [HttpGet]
        public IActionResult OtherAccessories(string Id)
        {
            var pvm = _productService.ReteriveAll().Where(w => w.TypeId == Id).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
            var spvm = _shopProductService.ReteriveAll().Select(s => new ShopProductViewModel
            {
                Id = s.Id,
                Shop_Id = s.Shop_Id,
                Product_Id = s.Product_Id,
                Image_Id = s.Image_Id,
                ShopName = _shopService.GetById(s.Shop_Id).Name,
                ProductName = _productService.GetById(s.Product_Id).Name,
                ImageName = _imageService.GetById(s.Image_Id).ImageName,
                StockCount = s.StockCount,
                Description = s.Description,

            }).ToList();
            IList<ShopProductResultViewModel> sprvm = (from t in pvm
                                                       join s in spvm on t.Id equals s.Product_Id
                                                       select new ShopProductResultViewModel
                                                       {
                                                           Id = s.Id,
                                                           Shop_Id = s.Shop_Id,
                                                           Product_Id = s.Product_Id,
                                                           Image_Id = s.Image_Id,
                                                           ShopName = _shopService.GetById(s.Shop_Id).Name,
                                                           ProductName = _productService.GetById(s.Product_Id).Name,
                                                           ImageName = _imageService.GetById(s.Image_Id).FrontImageUrl,
                                                           StockCount = s.StockCount,
                                                           Description = s.Description,
                                                           Unitprice = _productService.GetById(s.Product_Id).UnitPrice,

                                                       }).ToList();


            return View("SearchList", sprvm);
        }
    }
    }
