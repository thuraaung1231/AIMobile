using AIMobile.Models.DataModels;
using AIMobile.Services.Domains;
using AIMobileCus.Models.ViewModels;
using AIMobileCus.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotiService _notiService;
        private readonly IPurchaseService _purchaseService;

        public NotificationController(INotiService notiService ,IPurchaseService purchaseService)
        {
            _notiService = notiService;
            _purchaseService = purchaseService;
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
            IList<NotiViewModel> noti = _notiService.RetrieveAll().Where(w=>w.Status== "Pending....").Select(n => new NotiViewModel
            {
                Id = n.Id,
                ShopProductId = n.ShopProductId,
                CustomerId  = n.CustomerId, 
                PurchaseDateTime = n.PurchaseDateTime,  
                TotalPrice = n.TotalPrice,
                ScreenShot = n.ScreenShot,  
                PaymentTypeId = n.PaymentTypeId,
                DeliId = n.DeliId,
                TransactionId = n.TransactionId,  
                Status = n.Status,
                

            }).ToList();

            return View (noti);
        }
        public IActionResult Noticonfirm(string NotiId) {

            var noti=_notiService.GetById(NotiId);
            
            noti.Status = "Approved";
            noti.UpdatedAt = DateTime.Now;
            PurchaseEntity purchase = new PurchaseEntity() {
                Id = noti.Id,
                ShopProductId = noti.ShopProductId,
                CustomerId = noti.CustomerId,
                TotalPrice = noti.TotalPrice,
                ScreenShot = noti.ScreenShot,
                PaymentTypeId = noti.PaymentTypeId,
                DeliId = noti.DeliId,
                TransactionId = noti.TransactionId,
               PurchaseDateTime =noti.PurchaseDateTime,
                CreatedAt = DateTime.Now,
            
            };
            _purchaseService.Entry(purchase);
            _notiService.Update(noti);
            return RedirectToAction("Noti");
        }
    }
}
