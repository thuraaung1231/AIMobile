using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Controllers;
using AIMobileCus.Models.ViewModels;
using AIMobileCus.Services.Utilities;
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
        private readonly IPaymentTypeService _paymentTypeService;

        public ShopProductController(IShopProductService shopProductService, IShopService shopService, IProductService productService, IImageService imageService, ITypeServices typeServices,IPaymentTypeService paymentTypeService)
        {
            _shopProductService = shopProductService;
            _shopService = shopService;
            _productService = productService;
            _imageService = imageService;
            _typeServices = typeServices;
            _paymentTypeService = paymentTypeService;
        }

        [HttpPost]
        public JsonResult ViewShopProduct(string ProductId, string ImageId)
        {
            ShopProductEntity shopProductEntity = _shopProductService.GetShopProduct(ProductId, ImageId);
            var typeId = _productService.GetById(ProductId).TypeId;
            ShopProductViewModel shopProductViewModel = new ShopProductViewModel()
            {
                Id = shopProductEntity.Id,
                ProductId = shopProductEntity.ProductId,
                ImageId = shopProductEntity.ImageId,
                Description = shopProductEntity.Description,
                StockCount = shopProductEntity.StockCount,
                TypeId = typeId,
                ShopName=_shopService.GetById(shopProductEntity.ShopId).Name,
            };
            
            return Json(shopProductViewModel);
        }
        public IActionResult DetailProduct(string shopProductData)
        {
            var shopProductViewModel = JsonConvert.DeserializeObject<ShopProductViewModel>(shopProductData);
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

            //ForNav Bar
            IList<ProductViewModel> productView = _productService.ReteriveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                TypeId = p.TypeId,
                BrandId = p.BrandId,
                UnitPrice = p.UnitPrice,

            }).ToList();
            ViewBag.AllProductList = productView; /*for All Product*/

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
            
        //For Search Related Item
        IList<ImageViewModel> RelatedImages=new List<ImageViewModel>();
        IList<ProductViewModel>RelatedProducts=new List<ProductViewModel>();
         IList<ProductViewModel> productViewModels=_productService.ReteriveAll().Where(p=>p.TypeId== shopProductViewModel.TypeId).Select(s=>new ProductViewModel
         {
             Id=s.Id,
             UnitPrice = s.UnitPrice,
             Name = s.Name,
             TypeId=s.TypeId,
         }).ToList();

            foreach (var product in productViewModels)
            {
                var shopProduct = _shopProductService.GetShopProductByProductId(product.Id);
                if (shopProduct != null)
                {
                    var ImageId = shopProduct.ImageId;
                    if (!string.IsNullOrEmpty(ImageId))
                    {
                        // ImageId is not empty, proceed with retrieving the image
                        ImageEntity imageEntity = _imageService.GetById(ImageId);
                        ImageViewModel imageViewModel = new ImageViewModel()
                        {
                            Id = imageEntity.Id,
                            FrontImageUrl = imageEntity.FrontImageUrl,
                        };
                        RelatedImages.Add(imageViewModel);
                        RelatedProducts.Add(product);
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
            //For Show User Select Item
          
            ViewBag.Image = _imageService.GetById(shopProductViewModel.ImageId);
            ViewBag.Product = _productService.GetById(shopProductViewModel.ProductId);
            ViewBag.shopProduct = shopProductViewModel;

            var Descriptions = shopProductViewModel.Description;

            string[] DescriptionArray = Descriptions.Split(',');
            List<string> DescriptionKeyList = new List<string>();
            List<string> DescriptionValueList = new List<string>();
            foreach (var description in DescriptionArray)
            {
                int separatorIndex = description.IndexOf("-");
                DescriptionKeyList.Add(description.Substring(0, separatorIndex));
                DescriptionValueList.Add(description.Substring(separatorIndex+1).Trim());
            }
            ViewBag.DescriptionKey = DescriptionKeyList;
            ViewBag.DescriptionValue=DescriptionValueList;
            ViewBag.RelatedImages = RelatedImages;
            return View("DetailProduct",RelatedProducts);
        }
        [HttpPost]
        public JsonResult cart(CartViewModel selectedItem) {
          
            List<CartViewModel> cartViews;
            if (!HttpContext.Session.Keys.Contains("cart") ) {
                
                cartViews = new List<CartViewModel>();   
                cartViews.Add(selectedItem);
                
                SessionHelper.SetDataToSession(HttpContext.Session, "cart", cartViews);
            }
            else
            {

                cartViews = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");
                bool itemExists = false;
                foreach (var cartView in cartViews)
                {
                    if(cartView.Id== selectedItem.Id)
                    {
                        var shopProduct=_shopProductService.GetById(selectedItem.Id);
                       int totalItems= cartView.numberOfItem + selectedItem.numberOfItem;
                        if (totalItems>shopProduct.StockCount)
                        {
                            TempData["Message"] = "You can order under "+ shopProduct.StockCount+" Items";
                        }
                        else
                        {
                            cartView.numberOfItem += selectedItem.numberOfItem;
                        }
                        itemExists = true;
                        break;
                    }
                    
                }
                if (!itemExists)
                {
                    cartViews.Add(selectedItem);
                }
                SessionHelper.SetDataToSession(HttpContext.Session, "cart", cartViews);

            }


            return Json(cartViews);
        }
        [HttpPost]
        public JsonResult CartIcon() {

            var data = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");
          
        return Json( data);
        
        }
        [HttpPost]
        public JsonResult DeleteItem(string value)
        {
            
            List<CartViewModel> cartViews = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");
           for (int i=0;i< cartViews.Count;i++)
            {
                if (cartViews[i].Id == value)
                {
                    cartViews.Remove(cartViews[i]);
                }
            }
            SessionHelper.SetDataToSession(HttpContext.Session, "cart", cartViews);
            List<CartViewModel> resultcartviews = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");

            return Json(resultcartviews);
        }
        public JsonResult BuyNow(object value) { 
        
        return Json(null);
        }

        [HttpGet]
        public IActionResult AddtoCart(string cart)
        
       {
            IList<CartViewModel> TotalCart = JsonConvert.DeserializeObject<List<CartViewModel>>(cart);

            return View(TotalCart);  
        }
        [HttpGet]
        public IActionResult CheckOut() {
            IList<PaymentTypeViewModel> paymentTypeViews= _paymentTypeService.ReteriveAll().Select(p=>new PaymentTypeViewModel
            {
                Id= p.Id,
                PaymentType = p.PaymentType,
               PaymentTypeImage = p.PaymentTypeImage,
               PaymentTypeQR=p.PaymentTypeQR,
            }).ToList();
            ViewBag.PaymentTypes= paymentTypeViews;
            List<CartViewModel> resultcartviews = SessionHelper.GetDataFromSession<List<CartViewModel>>(HttpContext.Session, "cart");

            return View(resultcartviews);
        }
    }
}
