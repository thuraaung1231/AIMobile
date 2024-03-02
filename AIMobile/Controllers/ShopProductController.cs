using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class ShopProductController : Controller
    {
        private readonly IShopProductService _shopProductService;
        private readonly IShopService _shopService;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ITypeServices _typeServices;

        public ShopProductController(IShopProductService shopProductService,IShopService shopService,IProductService productService,IImageService imageService,ITypeServices typeServices)
        {
            _shopProductService = shopProductService;
            _shopService = shopService;
            _productService = productService;
            _imageService = imageService;
            _typeServices = typeServices;
        }
        public IActionResult Entry()
        {
            IList<TypeViewModel> typeViewModel=_typeServices.ReteriveAll().Select(p=>new TypeViewModel
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            
            
            return View(typeViewModel);
        }
        [HttpGet]
        public IActionResult EntryShopProduct(string id)
        {
            ViewBag.Shop = _shopService.ReteriveAll().ToList();
            ViewBag.Product = _productService.ReteriveAll().Where(p=>p.TypeId==id).ToList();
            ViewBag.Image = _imageService.ReteriveAll().ToList();
            TypeEntity typeEntity  =_typeServices.GetById(id);
            TypeViewModel typeViewModel = new TypeViewModel()
            {
                Id = typeEntity.Id,
                Name = typeEntity.Name,
            };
            return View(typeViewModel);
        }
        [HttpPost]
        public IActionResult Entry(ShopProductViewModel spv) {
            try
            {
                ShopProductEntity shopProductEntity = new ShopProductEntity()
                {

                    Id = Guid.NewGuid().ToString(),
                    Shop_Id = spv.Shop_Id,
                    Image_Id = spv.Image_Id,
                    Product_Id = spv.Product_Id,
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
            var shopProductEntity=_shopProductService.GetById("6b0dc9df-ecc5-4b0b-a2cd-a4b6eb9061b4");
            ShopProductViewModel shopProductViewModel = new ShopProductViewModel()
            {
                Description = shopProductEntity.Description,
                StockCount = shopProductEntity.StockCount,
            };
            return View(shopProductViewModel);
        }
    }
}
