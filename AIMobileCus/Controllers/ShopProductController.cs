using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;

namespace AIMobile.Controllers
{
    public class ShopProductController : Controller
    {
        private readonly IShopProductService _shopProductService;
        private readonly IShopService _shopService;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ITypeServices _typeServices;

        public ShopProductController(IShopProductService shopProductService, IShopService shopService, IProductService productService, IImageService imageService, ITypeServices typeServices)
        {
            _shopProductService = shopProductService;
            _shopService = shopService;
            _productService = productService;
            _imageService = imageService;
            _typeServices = typeServices;
        }

        [HttpPost]
        public JsonResult ViewShopProduct(string ProductId, string ImageId)
        {
            ShopProductEntity shopProductEntity = _shopProductService.GetShopProduct(ProductId, ImageId);
            ShopProductViewModel shopProductViewModel = new ShopProductViewModel()
            {
                Id = shopProductEntity.Id,
                ProductId = shopProductEntity.ProductId,
                ImageId = shopProductEntity.ImageId,
                Description = shopProductEntity.Description,
                StockCount = shopProductEntity.StockCount,
            };
            //ViewBag.ShopProduct=_shopProductService.GetShopProduct(ProductId,ImageId);
            //ViewBag.Image = _imageService.GetById(ImageId);
            //ViewBag.Product = _productService.GetById(ProductId);
            return Json(shopProductViewModel);
        }
        public IActionResult DetailProduct(string shopProductData)
        {
            var shopProductViewModel = JsonConvert.DeserializeObject<ShopProductViewModel>(shopProductData);
            ViewBag.Image = _imageService.GetById(shopProductViewModel.ImageId);
            ViewBag.Product = _productService.GetById(shopProductViewModel.ProductId);
            ViewBag.shopProduct = shopProductViewModel;
            ViewBag.Descriptions = shopProductViewModel.Description;
            
            //string[] array = Descriptions.Split(',');
            //for(int i = 0; i < array.Length; i++)
            //{
            //    Console.WriteLine(Descriptions);
            //}

            //if (array.Length > 0) // Check if the array has elements
            //{
            //    string lastElement = array[array.Length - 1];
            //    Console.WriteLine(lastElement);
            //}

            return View();
        }
    }
}
