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
        //public IActionResult GetById(string id)
        //{
        //    var shopProductEntity=_shopProductService.GetById("6b0dc9df-ecc5-4b0b-a2cd-a4b6eb9061b4");
        //    ShopProductViewModel shopProductViewModel = new ShopProductViewModel()
        //    {
        //        Description = shopProductEntity.Description,
        //        StockCount = shopProductEntity.StockCount,
        //    };
        //    return View(shopProductViewModel);
        //}

        public IActionResult List()
        {
            List<ShopProductViewModel> shopProduct = _shopProductService.ReteriveAll().Select(s => new ShopProductViewModel
            {
Id=s.Id,
ImageName=_imageService.GetById(s.ImageId).ImageName,
                ShopName = _shopService.GetById(s.ShopId).Name,
                ProductName =_productService.GetById(s.ProductId).Name,
Description = s.Description,
StockCount = s.StockCount,

            }).ToList();
            return View(shopProduct);
        }
        public IActionResult Delete(string id)
        {
            try
            {
                _shopProductService.Delete(id);
                TempData["Info"] = "Successfully delete the record to the system";
 
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when delete the record to the sytem";
               
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string id) 
        {
        var shopProductDataModel=_shopProductService.GetById(id);
            ShopProductViewModel shopProduct = new ShopProductViewModel()
            {
                ImageId=shopProductDataModel.ImageId,
                ImageName=_imageService.GetById(shopProductDataModel.ImageId).ImageName,
                ProductId=shopProductDataModel.ProductId,
                ProductName=_productService.GetById(shopProductDataModel.ProductId).Name,
                
                ShopId=shopProductDataModel.ShopId,
                ShopName=_shopService.GetById(shopProductDataModel.ShopId).Name,
                Description = shopProductDataModel.Description,
                StockCount = shopProductDataModel.StockCount,
            };
            ViewBag.Shop = _shopService.ReteriveAll().Where(w=>w.Id!=shopProduct.ShopId).Select(s=>new ShopViewModel { Id=s.Id,Name=s.Name}).ToList();
            ViewBag.Product = _productService.ReteriveAll().Where(w=>w.Id!=shopProduct.ProductId).Select(s=>new ProductViewModel { Id=s.Id,Name=s.Name}).ToList();
            ViewBag.Image = _imageService.ReteriveAll().Where(w => w.Id != shopProduct.ImageId).Select(s => new ImageViewModel { Id = s.Id, ImageName = s.ImageName }).ToList();
            return View(shopProduct);
        
        }
        [HttpPost]
        public IActionResult Update(ShopProductViewModel spv)
        {
            try
            {
                ShopProductEntity shop = new ShopProductEntity()
                {
                    ImageId = spv.ImageId,
                    ProductId = spv.ProductId,
                    ShopId = spv.ShopId,
                    Description = spv.Description,
                    StockCount = spv.StockCount,
                };
                _shopProductService.Update(shop);
                TempData["Info"] = "Save successfully the record to the systrm";
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error occur when the record to the ystem";
               
            }
            return RedirectToAction("List");
        }

    }
}
