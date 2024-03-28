using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models;
using AIMobileCus.Models.ViewModels;
using AIMobileCus.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
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
            //For Cart
            int count;
            List<CartViewModel> cartviews = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");
            if (cartviews == null)
            {
                count = 0;
            }
            else
            {
                count = cartviews.Count;
            }

            TempData["count"] = count;


            //for Nav DropDown

            IList<ProductViewModel> productViewModels = _productService.ReteriveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                TypeId = p.TypeId,
                BrandId = p.BrandId,
                UnitPrice = p.UnitPrice,

            }).ToList();
            ViewBag.ProductList = productViewModels; /*for All Product*/

            IList<TypeViewModel> typeViewModels = _typeServices.ReteriveAll().Select(p => new TypeViewModel
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            var PhoneId = "";
            var LapTopId = "";
            var TabletId = "";
            var SmartWatchId = "";
            var HeadPhoneId = "";
            var PowerBankId = "";
            foreach(var type in typeViewModels)
            {
                if (type.Name == "Mobile")
                {
                    PhoneId = type.Id;
                }else if (type.Name == "Laptop")
                {
                    LapTopId=type.Id;
                }else if(type.Name == "Tablet")
                {
                    TabletId = type.Id;
                }else if(type.Name=="Smart Watch")
                {
                    SmartWatchId= type.Id;
                }else if(type.Name == "HeadPhone")
                {
                    HeadPhoneId = type.Id;
                }else if (type.Name == "PowerBank")
                {
                    PowerBankId = type.Id;
                }
            }
            //for Phone Product
            IList<ProductViewModel> PhoneViewModel = _productService.ReteriveAll().Where(p=>p.TypeId==PhoneId).Select(s => new ProductViewModel
            {
                Id=s.Id,
                Name=s.Name,
                TypeId=s.TypeId,
                BrandId=s.BrandId,
            }).ToList();
            ViewBag.Phones=PhoneViewModel;

            IList<ProductViewModel> LaptopViewModel = _productService.ReteriveAll().Where(p => p.TypeId == LapTopId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.Laptops=LaptopViewModel;

            IList<ProductViewModel> TabletViewModel = _productService.ReteriveAll().Where(p => p.TypeId == TabletId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.tablets = TabletViewModel;

            IList<ProductViewModel> SmartWatchViewModel = _productService.ReteriveAll().Where(p => p.TypeId == SmartWatchId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.smartWatchs = SmartWatchViewModel;

            IList<ProductViewModel>OtherAccessoryViewModel= _productService.ReteriveAll().Where(p=>p.TypeId!=PhoneId&&p.TypeId!=TabletId&&p.TypeId!=LapTopId&&p.TypeId!=SmartWatchId).Select(s=>new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
           
            ViewBag.OtherAccessories=OtherAccessoryViewModel;


            //For the Most Populat Items
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


            //for search with the type
            
            ViewBag.LaptopId = LapTopId;
            ViewBag.PhoneId=PhoneId;
            ViewBag.TabletId = TabletId;
            ViewBag.SmartWatchId = SmartWatchId;
            ViewBag.PowerBankId = PowerBankId;
            ViewBag.HeadPhoneId = HeadPhoneId;
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

        
       public JsonResult SearchProduct(string search)
        {
            IList<ShopProductResultViewModel>shopProductResults = new List<ShopProductResultViewModel>();
            IList<ProductViewModel> productViewModel = _productService.ReteriveAll().Select(p=>new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,    
            }).ToList();
            IList<ShopProductViewModel>shopProducts=_shopProductService.ReteriveAll().Select(s=>new ShopProductViewModel
            {
                Id=s.Id,
                ImageId=s.ImageId,
                ProductId=s.ProductId,
            }).ToList();
            foreach(var shopProduct in shopProducts)
            {
                ShopProductResultViewModel shopProductResultView = new ShopProductResultViewModel
                {
                    ProductId = shopProduct.ProductId,
                    ImageId=shopProduct.ImageId,
                    Name=_productService.GetById(shopProduct.ProductId).Name,
                    FrontImageUrl=_imageService.GetById(shopProduct.ImageId).FrontImageUrl,
                    ShopProductId=shopProduct.Id,
                };
                shopProductResults.Add(shopProductResultView);
            }
            return Json(shopProductResults);
        }


        public IActionResult FreeShipping()
        {
            return View();
        }

        public IActionResult ReturnPolicy()
        {
            return View();
        }

        public IActionResult EasyPayment()
        {
            return View();
        }

        public IActionResult FaceDelivery()
        {
            return View();
        }
    }


       

    }
