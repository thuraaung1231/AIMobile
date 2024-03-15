using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobileCus.Controllers
{
    public class MainPageController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShopProductService _shopProductService;
        private readonly IImageService _imageService;
        private readonly IShopService _shopService;
        private readonly ITypeServices _typeServices;

        public MainPageController(IProductService productService,IShopProductService shopProductService,IImageService imageService,IShopService shopService,ITypeServices typeServices)
        {
           _productService = productService;
           _shopProductService = shopProductService;
            _imageService = imageService;
            _shopService = shopService;
            _typeServices = typeServices;
        }
        public IActionResult ShowProduct(string id)
        {
            //ForNav Bar
            IList<ProductViewModel> productViewModels = _productService.ReteriveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                TypeId = p.TypeId,
                BrandId = p.BrandId,
                UnitPrice = p.UnitPrice,

            }).ToList();
            ViewBag.AllProductList = productViewModels; /*for All Product*/

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
            foreach (var type in typeViewModels)
            {
                if (type.Name == "Phone")
                {
                    PhoneId = type.Id;
                }
                else if (type.Name == "Laptop")
                {
                    LapTopId = type.Id;
                }
                else if (type.Name == "Tablet")
                {
                    TabletId = type.Id;
                }
                else if (type.Name == "Smart Watch")
                {
                    SmartWatchId = type.Id;
                }
                else if (type.Name == "HeadPhone")
                {
                    HeadPhoneId = type.Id;
                }
                else if (type.Name == "PowerBank")
                {
                    PowerBankId = type.Id;
                }
            }
            //for Phone Product
            IList<ProductViewModel> PhoneViewModel = _productService.ReteriveAll().Where(p => p.TypeId == PhoneId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.Phones = PhoneViewModel;

            IList<ProductViewModel> LaptopViewModel = _productService.ReteriveAll().Where(p => p.TypeId == LapTopId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.Laptops = LaptopViewModel;

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

            IList<ProductViewModel> OtherAccessoryViewModel = _productService.ReteriveAll().Where(p => p.TypeId != PhoneId && p.TypeId != TabletId && p.TypeId != LapTopId && p.TypeId != SmartWatchId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();

            ViewBag.OtherAccessories = OtherAccessoryViewModel;

            // For filtering the product
            ViewBag.LaptopId = LapTopId;
            ViewBag.PhoneId = PhoneId;
            ViewBag.TabletId = TabletId;
            ViewBag.SmartWatchId = SmartWatchId;
            ViewBag.PowerBankId = PowerBankId;
            ViewBag.HeadPhoneId = HeadPhoneId;

            //for All Product
            IList<ShopProductViewModel>shopProducts=_shopProductService.ReteriveAll().Where(s=>s.ProductId==id).Select(p=>new ShopProductViewModel
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ImageId = p.ImageId,
                ShopId = p.ShopId,
                Description = p.Description,
                StockCount = p.StockCount,

            }).ToList();
            IList<ProductViewModel>products=new List<ProductViewModel>();
            IList<ImageViewModel>Images=new List<ImageViewModel>();
            IList<ShopViewModel>Shopes=new List<ShopViewModel>();
            IList<string>Descriptions=new List<string>();
            foreach (var ShopProduct in shopProducts)
            {
                var product = _productService.GetById(ShopProduct.ProductId);
                var Image = _imageService.GetById(ShopProduct.ImageId);
                var Shop=_shopService.GetById(ShopProduct.ShopId);
                
                ProductViewModel productView = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    TypeId = product.TypeId,
                    BrandId = product.BrandId,

                };
                products.Add(productView);
                ImageViewModel imageView = new ImageViewModel()
                {
                    Id = Image.Id,
                    ImageName = Image.ImageName,
                    FrontImageUrl= Image.FrontImageUrl,
                    BackImageUrl= Image.BackImageUrl,   
                    RightSideImageUrl= Image.RightSideImageUrl,
                    LeftSideImageUrl= Image.LeftSideImageUrl,
                };
                Images.Add(imageView);
                ShopViewModel shopView = new ShopViewModel()
                {
                    Id = Shop.Id,
                    Name = Shop.Name,
                };
                Shopes.Add(shopView);
                var Description = ShopProduct.Description;
                string[] dataArray = Description.Split(',');
                if (dataArray.Length > 0)
                {
                    // Remove the last element
                    Array.Resize(ref dataArray, dataArray.Length - 1);
                }
                string newData = string.Join(",", dataArray);
                Descriptions.Add(newData);
            }
            ViewBag.DescriptionList=Descriptions;
            ViewBag.ProductList=products;
            ViewBag.ImageList = Images;
            ViewBag.ShopList = Shopes;
            if(shopProducts.Count > 0)
            {
                return View("ShowAllProduct", shopProducts);
            }
            else
            {
                return RedirectToAction("ShowAllProduct");
            }
            
        }
      public IActionResult ShowAllProduct(string id)
        {

            //ForNav
            IList<ProductViewModel> AllProductViewModels = _productService.ReteriveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                TypeId = p.TypeId,
                BrandId = p.BrandId,
                UnitPrice = p.UnitPrice,

            }).ToList();
            ViewBag.AllProductList = AllProductViewModels;
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
            foreach (var type in typeViewModels)
            {
                if (type.Name == "Phone")
                {
                    PhoneId = type.Id;
                }
                else if (type.Name == "Laptop")
                {
                    LapTopId = type.Id;
                }
                else if (type.Name == "Tablet")
                {
                    TabletId = type.Id;
                }
                else if (type.Name == "Smart Watch")
                {
                    SmartWatchId = type.Id;
                }
                else if (type.Name == "HeadPhone")
                {
                    HeadPhoneId = type.Id;
                }
                else if (type.Name == "PowerBank")
                {
                    PowerBankId = type.Id;
                }
            }
            //for Phone Product
            IList<ProductViewModel> PhoneViewModel = _productService.ReteriveAll().Where(p => p.TypeId == PhoneId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.Phones = PhoneViewModel;

            IList<ProductViewModel> LaptopViewModel = _productService.ReteriveAll().Where(p => p.TypeId == LapTopId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.Laptops = LaptopViewModel;

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

            IList<ProductViewModel> OtherAccessoryViewModel = _productService.ReteriveAll().Where(p => p.TypeId != PhoneId && p.TypeId != TabletId && p.TypeId != LapTopId && p.TypeId != SmartWatchId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TypeId = s.TypeId,
                BrandId = s.BrandId,
            }).ToList();
            ViewBag.OtherAccessories = OtherAccessoryViewModel;

            //For filtering the product
            ViewBag.LaptopId = LapTopId;
            ViewBag.PhoneId = PhoneId;
            ViewBag.TabletId = TabletId;
            ViewBag.SmartWatchId = SmartWatchId;
            ViewBag.PowerBankId = PowerBankId;
            ViewBag.HeadPhoneId = HeadPhoneId;

            //For Showing the product
            IList<string> Descriptions = new List<string>();
            IList<ImageViewModel> RelatedImages = new List<ImageViewModel>();
            IList<ProductViewModel> RelatedProducts = new List<ProductViewModel>();
            IList<ShopProductViewModel>RelatedShowProducts=new List<ShopProductViewModel>();
            IList<ShopViewModel> Shopes = new List<ShopViewModel>();
            IList<ProductViewModel> productViewModels = _productService.ReteriveAll().Where(p => p.TypeId == id).Select(s => new ProductViewModel
            {
                Id = s.Id,
                UnitPrice = s.UnitPrice,
                Name = s.Name,
                TypeId = s.TypeId,
            }).ToList();
            
            foreach (var product in productViewModels)
            {
                var shopProductEntity = _shopProductService.GetShopProductByProductId(product.Id);
                if(shopProductEntity != null)
                {
                   
                    ShopProductViewModel shopProduct = new ShopProductViewModel()
                    {
                        Id = shopProductEntity.Id,
                        Description = shopProductEntity.Description,
                        ProductId = shopProductEntity.Id,
                        StockCount = shopProductEntity.StockCount,
                        ShopId = shopProductEntity.ShopId,
                        ImageId = shopProductEntity.ImageId,
                    };
                    //toadd Related images, related shop and related shopproducts
                    if (shopProduct != null)
                    {
                        //for the Description
                        var Description = shopProduct.Description;
                        string[] dataArray = Description.Split(',');
                        if (dataArray.Length > 0)
                        {
                            string[] newDataArray = new string[dataArray.Length - 1];
                            Array.Copy(dataArray, newDataArray, newDataArray.Length);
                            
                            string newData = string.Join(",", newDataArray);
                            Descriptions.Add(newData);
                        }                     
                        //for the products
                        var ShopId = shopProduct.ShopId;
                        var ImageId = shopProduct.ImageId;
                        if (!string.IsNullOrEmpty(ImageId))
                        {
                            // ImageId is not empty, proceed with retrieving the image
                            ImageEntity imageEntity = _imageService.GetById(ImageId);
                            ShopEntity shopEntity = _shopService.GetById(ShopId);
                            ImageViewModel imageViewModel = new ImageViewModel()
                            {
                                Id = imageEntity.Id,
                                FrontImageUrl = imageEntity.FrontImageUrl,
                            };
                            ShopViewModel shopViewModel = new ShopViewModel()
                            {
                                Id = shopEntity.Id,
                                Name = shopEntity.Name,
                                Address = shopEntity.Address,
                            };
                            RelatedImages.Add(imageViewModel);
                            RelatedProducts.Add(product);
                            RelatedShowProducts.Add(shopProduct);
                            Shopes.Add(shopViewModel);
                        }
                        else
                        {
                            // ImageId is empty, handle this case (optional)
                            ViewBag.err = "‌ေError occur";
                        }
                    }
                    else
                    {

                    }
                    
                }
                
            }
            ViewBag.ProductList = RelatedProducts;
            ViewBag.ImageList = RelatedImages;
            ViewBag.ShopList = Shopes;
            ViewBag.DescriptionList = Descriptions;
            return View(RelatedShowProducts);
        }
    }
}
