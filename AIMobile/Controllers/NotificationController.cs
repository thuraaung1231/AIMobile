using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models.DataModels;
using AIMobileCus.Models.ViewModels;
using AIMobileCus.Services.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace AIMobile.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotiService _notiService;
        private readonly IPurchaseService _purchaseService;
        private readonly IShopService _shopService;
        private readonly IProductService _productService;
        private readonly IShopProductService _shopProductService;
        private readonly IPaymentTypeService _paymentTypeService;

        public NotificationController(INotiService notiService ,IPurchaseService purchaseService,IShopService shopService,IProductService productService,IShopProductService shopProductService,IPaymentTypeService paymentTypeService)
        {
            _notiService = notiService;
            _purchaseService = purchaseService;
            _shopService = shopService;
           _productService = productService;
            _shopProductService = shopProductService;
            _paymentTypeService = paymentTypeService;
        }
        [HttpPost]
        public JsonResult NotiIcon()
        {
           IList<NotiViewModel>noti= _notiService.RetrieveAll().Where(s=>s.Status=="Pending....").Select(n=> new NotiViewModel
           {
               Id = n.Id,


           }).ToList();
            var count= noti.Count();    

            return Json( count);
        }
        [HttpGet]
        public IActionResult Noti() {
            IList<NotiViewModel> notiViewList=new List<NotiViewModel>();
            var customerId = "";
            var TransactionId = "";
            IList<NotiEntity> notiEntities = _notiService.RetrieveAll().Where(w => w.Status == "Pending....").ToList();
            if (notiEntities.Count == 0)
            {
                ViewBag.EmptyNoti = "Empty!";
                return View();
            }
            NotiViewModel notiViewModel = new NotiViewModel();
            foreach(var noti in notiEntities)
            {
                if (customerId == "" && TransactionId == "")
                {
                    notiViewModel.Id = noti.Id;
                    notiViewModel.CustomerId = noti.CustomerId;
                    notiViewModel.CustomerEmail = noti.CustomerEmail;
                    notiViewModel.PaymentTypeId = noti.PaymentTypeId;
                    notiViewModel.PurchaseDateTime = noti.PurchaseDateTime;
                    notiViewModel.ScreenShot = noti.ScreenShot;
                    notiViewModel.PaymentTypeId = noti.PaymentTypeId;
                    notiViewModel.TransactionId = noti.TransactionId;
                    notiViewModel.PaymentTypeName = _paymentTypeService.GetById(noti.PaymentTypeId).PaymentType;
                    notiViewModel.Status = noti.Status;
                    var shopProduct = _shopProductService.GetById(noti.ShopProductId);
                    TransactionViewModel transactionView = new TransactionViewModel()
                    {
                        ShopProductId = noti.ShopProductId,

                        ProductName = _productService.GetById(shopProduct.ProductId).Name,
                        ShopName = _shopService.GetById(shopProduct.ShopId).Name,
                        TotalPrice = noti.TotalPrice,
                        NumberOfItem=noti.NumberOfItem,

                    };
                    notiViewModel.TransactionList = new List<TransactionViewModel>();
                    notiViewModel.TransactionList.Add(transactionView);
                }
                else if(noti.TransactionId != TransactionId)
                {
                    notiViewList.Add(notiViewModel);
                    notiViewModel = new NotiViewModel();
                    notiViewModel.Id = noti.Id;
                    notiViewModel.CustomerId = noti.CustomerId;
                    notiViewModel.CustomerEmail = noti.CustomerEmail;
                    notiViewModel.PaymentTypeId = noti.PaymentTypeId;
                    notiViewModel.TransactionId = noti.TransactionId;
                    notiViewModel.PurchaseDateTime = noti.PurchaseDateTime;
                    notiViewModel.ScreenShot=noti.ScreenShot;
                    notiViewModel.PaymentTypeId=noti.PaymentTypeId;
                    notiViewModel.PaymentTypeName=_paymentTypeService.GetById(noti.PaymentTypeId).PaymentType;
                    notiViewModel.Status = noti.Status;
                    var shopProduct = _shopProductService.GetById(noti.ShopProductId);
                    TransactionViewModel transactionView = new TransactionViewModel() {
                        ShopProductId = noti.ShopProductId,

                        ProductName = _productService.GetById(shopProduct.ProductId).Name,
                        ShopName=_shopService.GetById(shopProduct.ShopId).Name,
                        TotalPrice = noti.TotalPrice,
                        NumberOfItem = noti.NumberOfItem,
                        
                    };
                    notiViewModel.TransactionList = new List<TransactionViewModel>();
                    notiViewModel.TransactionList.Add(transactionView);
                }
                else
                {
                    var shopProduct = _shopProductService.GetById(noti.ShopProductId);
                    TransactionViewModel transactionView = new TransactionViewModel()
                    {
                        ShopProductId = noti.ShopProductId,

                        ProductName = _productService.GetById(shopProduct.ProductId).Name,
                        ShopName = _shopService.GetById(shopProduct.ShopId).Name,
                        TotalPrice = noti.TotalPrice,
                        NumberOfItem = noti.NumberOfItem,
                    };
                    notiViewModel.TransactionList.Add(transactionView);

                }
                customerId = noti.CustomerId;
                TransactionId = noti.TransactionId;
            }
            if (notiViewModel != null)
            {
                notiViewList.Add(notiViewModel);
            }

            return View (notiViewList);
        }
        public IActionResult Noticonfirm(string TransactionId) {

            IList<NotiEntity> notiEntities=_notiService.RetrieveByTransactionId(TransactionId);
            foreach(var notiEntity in notiEntities)
            {
                notiEntity.Status = "Approved";
                notiEntity.UpdatedAt = DateTime.Now;
                PurchaseEntity purchase = new PurchaseEntity()
                {
                    Id = notiEntity.Id,
                    ShopProductId = notiEntity.ShopProductId,
                    CustomerId = notiEntity.CustomerId,
                    TotalPrice = notiEntity.TotalPrice,
                    ScreenShot = notiEntity.ScreenShot,
                    PaymentTypeId = notiEntity.PaymentTypeId,
                    DeliId = notiEntity.DeliId,
                    TransactionId = notiEntity.TransactionId,
                    PurchaseDateTime = notiEntity.PurchaseDateTime,
                    CreatedAt = DateTime.Now,

                };
                _purchaseService.Entry(purchase);
                _notiService.Update(notiEntity);
            }
            
            return RedirectToAction("Noti");
        }
       
    }
}
