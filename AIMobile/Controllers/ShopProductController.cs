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

        public IActionResult List()
        {
            IList<ShopProductViewModel> shopProducts = _shopProductService.ReteriveAll().Select(sp => new ShopProductViewModel
            {
                Id = sp.Id,
                ShopId = sp.ShopId,
                ProductId = sp.ProductId,
                ImageId=sp.ImageId,
                ShopName=_shopService.GetById(sp.ShopId)?.Name,
                ProductName=_productService.GetById(sp.ProductId)?.Name,
                ImageName=_imageService.GetById(sp.ImageId)?.ImageName,
                Description = sp.Description,
                StockCount=sp.StockCount,

            }).ToList();
            return View(shopProducts);
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
            ShopProductViewModel shopProduct = new ShopProductViewModel();
            var SPDataModel = _shopProductService.GetById(id);
            if (SPDataModel != null)
            {
                shopProduct.Id = SPDataModel.Id;
                shopProduct.ShopId = SPDataModel.ShopId;
                shopProduct.ProductId = SPDataModel.ProductId;
                shopProduct.ImageId=SPDataModel.ImageId;
                shopProduct.ShopName=_shopService.GetById(SPDataModel.ShopId).Name;
                shopProduct.ProductName=_productService.GetById(SPDataModel.ProductId).Name;
                shopProduct.ImageName = _imageService.GetById(SPDataModel.ImageId).ImageName;
                shopProduct.Description= SPDataModel.Description;
                shopProduct.StockCount=SPDataModel.StockCount;
            }
            ViewBag.FromShopId = _shopService.ReteriveAll().Select(s => new ShopViewModel { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.FormProductId=_productService.ReteriveAll().Select(s=>new ProductViewModel { Id=s.Id, Name=s.Name }).ToList();
            ViewBag.FormImageId=_imageService.ReteriveAll().Select(s=>new ImageViewModel { Id=s.Id,ImageName=s.ImageName}).ToList();
            return View(shopProduct);
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
    }
}
