using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models;
using AIMobileCus.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
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
            List<string> ImageIds= new List<string>();
            List<string> ProductIds=new List<string>();
            IList<ProductViewModel> Products=new List<ProductViewModel>();
            IList<ImageViewModel> Images=new List<ImageViewModel>();
            
            IList<ShopProductViewModel> shopProductViewModels=_shopProductService.ReteriveAll().Select(s=>new ShopProductViewModel
            {
                Id = s.Id,
                ImageId = s.ImageId,
                ShopId= s.ShopId,
                ProductId= s.ProductId,
                Description= s.Description,
                StockCount  = s.StockCount,
                
            }).ToList();
            foreach(var shopProduct in shopProductViewModels)
            {
                ImageIds.Add(shopProduct.ImageId);
                ProductIds.Add(shopProduct.ProductId);
            }
            foreach(var productId in ProductIds)
            {
                var productEntity=_productService.GetById(productId);
               
                    
                        ProductViewModel productViewModel = new ProductViewModel()
                        {
                            Id = productEntity.Id,
                            Name = productEntity.Name,
                            UnitPrice = productEntity.UnitPrice,
                            TypeId = productEntity.TypeId,
                            BrandId = productEntity.BrandId,
                        };
                        Products.Add(productViewModel);
                    
               
                
            }
            foreach(var imageId in ImageIds)
            {
                var imageEntity=_imageService.GetById(imageId);
               
                    ImageViewModel imageViewModel = new ImageViewModel()
                    {
                        Id = imageEntity.Id,
                        FrontImageUrl = imageEntity.FrontImageUrl,
                        BackImageUrl = imageEntity.BackImageUrl,
                        LeftSideImageUrl = imageEntity.LeftSideImageUrl,
                        RightSideImageUrl = imageEntity.RightSideImageUrl,
                        Filesize = imageEntity.Filesize,
                        Filetype = imageEntity.Filetype,
                    };
                    Images.Add(imageViewModel);
               
               
            }
            ProductImageViewModel ProductImages=new ProductImageViewModel();
            ProductImages.Images = Images;
            ProductImages.Products = Products;
            return View(ProductImages);
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