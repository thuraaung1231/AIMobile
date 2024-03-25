using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models.DataModels;
using AIMobileCus.Models.ViewModels;
using AIMobileCus.Services.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AIMobileCus.Controllers
{
    public class ShopHistory : Controller
    {
        private readonly INotiService _notiService;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IShopProductService _shopProductService;
        private readonly IShopService _shopService;

        public ShopHistory(INotiService notiService,IProductService productService,IImageService imageService,IShopProductService shopProductService,IShopService shopService)
        {
            _notiService = notiService;
            _productService = productService;
            _imageService = imageService;
            _shopProductService = shopProductService;
            _shopService = shopService;
        }
        public IActionResult ViewShopHistory(string userId)
        {
            IList<ProductViewModel> productViewModels = new List<ProductViewModel>();
            //IList<ShopProductViewModel> shopProductViewModels = new List<ShopProductViewModel>();
            IList<ImageViewModel> imageViewModels=new List<ImageViewModel>();
            IList<ShopViewModel> shopViewModels=new List<ShopViewModel>();
            IList<NotiViewModel>notiViews=_notiService.RetrieveAll(userId).Select(n=>new NotiViewModel
            {
                Id = n.Id,
                CustomerId = n.CustomerId,
                ShopProductId = n.ShopProductId,
                PurchaseDateTime = n.PurchaseDateTime,
                TotalPrice= n.TotalPrice,
                PaymentTypeId = n.PaymentTypeId,
                Status = n.Status,
                UpdatedAt = n.UpdatedAt,
            }).ToList();
            foreach(var noti in notiViews)
            {
                var shopProduct=_shopProductService.GetById(noti.ShopProductId);

                var product = _productService.GetById(shopProduct.ProductId);
                var image = _imageService.GetById(shopProduct.ImageId);
                var shop = _shopService.GetById(shopProduct.ShopId);

                ImageViewModel imageView = new ImageViewModel
                {
                    Id = image.Id,
                    FrontImageUrl = image.FrontImageUrl,
                };
                imageViewModels.Add(imageView);
                ProductViewModel productView = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                };
                productViewModels.Add(productView);
                ShopViewModel shopView = new ShopViewModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                };
                shopViewModels.Add(shopView);
            }
            ViewBag.Images = imageViewModels;
            ViewBag.Shops= shopViewModels;
            ViewBag.Products = productViewModels;
            return View(notiViews);
        }
    }
}
