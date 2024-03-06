using AIMobile.Models.DataModels;
using AIMobileCus.Services.Domains;
using AIMobileCus.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Sockets;
using System.Text.Json;

namespace AIMobileCus.Controllers
{
    public class CartPurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        public CartPurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        public ActionResult Entry()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Purchase(PurchaseEntity purchase)
        {
            // temp data
            PurchaseEntity[] dataArr  = new PurchaseEntity[]
            {
               new PurchaseEntity { TotalPrice = 500000.00, ShopProduct_Id = "04ba0eb3-e6fc-4f4a-8542-49b83e54ce40",Customer_Id = "C-0000001",PurchaseDateTime = DateTime.Now, PaymentType_Id = "PT-001",ScreenShot = "",Deli_Id = "D-0000001",CreatedAt = DateTime.Now,},
			   new PurchaseEntity { TotalPrice = 100000.00, ShopProduct_Id = "0cb6be59-2ef1-473d-be5e-fffab25f54bd",Customer_Id = "C-0000001",PurchaseDateTime = DateTime.Now,PaymentType_Id = "PT-001",ScreenShot = "",Deli_Id = "D-0000001",CreatedAt = DateTime.Now,},
			   new PurchaseEntity { TotalPrice = 1500000.00, ShopProduct_Id = "0d7788a6-3531-43ee-ba24-745fa970662f",Customer_Id = "C-0000001",PurchaseDateTime = DateTime.Now,PaymentType_Id = "PT-001",ScreenShot = "",Deli_Id = "D-0000001",CreatedAt = DateTime.Now,}
			};

           
			// input data to session
			SessionHelper.SetDataToSession(HttpContext.Session,"cartInfo", dataArr);

            
			ArrayList purchaseList = SessionHelper.GetDataFromSession<PurchaseEntity>(HttpContext.Session,"cartInfo");

            if(purchaseList != null)
            {
                for(int i = 0; i < purchaseList.Count; i++)
                {
					if (purchaseList[i] is PurchaseEntity purchaseEntity) // Check if the item is of type PurchaseEntity
					{
						_purchaseService.Create(purchaseEntity); // Call the Create method of _purchaseService with the PurchaseEntity object
					}
				}
            }

			return Json(new { Message = "Successfully Save...." });
        }
    }
}
