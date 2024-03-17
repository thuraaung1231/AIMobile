using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IImageService _imageService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService,IImageService imageService)
        {
            _paymentTypeService = paymentTypeService;
            _imageService = imageService;
        }
        public IActionResult Entry()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entry(PaymentTypeViewModel pvm)
        {
            try
            {
                string imgUrl = "/Images/";
                var PaymentTypeEntity = new PaymentTypeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    PaymentType = pvm.PaymentType,
                    PaymentTypeImage = imgUrl+ pvm.PaymentTypeImage,
                    PaymentTypeQR  = imgUrl+ pvm.PaymentTypeQR,
                };
                _paymentTypeService.Entry(PaymentTypeEntity);
                TempData["info"] = "Save Successfully the record to the system";
              
            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur when the data record to the system";
            }
            return View();
        }

        public IActionResult List()
        {
            try
            {
                IList<PaymentTypeViewModel> payments = _paymentTypeService.ReteriveAll().Select(u => new PaymentTypeViewModel
                {
                    Id = u.Id,
                    PaymentType = u.PaymentType,
                    PaymentTypeImage = u.PaymentTypeImage,
                    PaymentTypeQR = u.PaymentTypeQR,
                }).ToList();
                return View(payments);
            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur ";
            }
            return View();
        }


        public IActionResult Delete(string id)
        {
            try
            {
                _paymentTypeService.Delete(id);
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
            try
            {
                PaymentTypeViewModel payment = new PaymentTypeViewModel();
                var PaymentDataModel = _paymentTypeService.GetById(id);
                if (PaymentDataModel != null)
                {
                    payment.Id = PaymentDataModel.Id;
                    payment.PaymentType = PaymentDataModel.PaymentType;
                    payment.PaymentTypeImage = PaymentDataModel.PaymentTypeImage;
                    payment.PaymentTypeQR = PaymentDataModel.PaymentTypeQR;
                }
                return View(payment);
            }
            catch (Exception e)
            {
                TempData["info"] = "Error occur ";
            }
            return View();
        }


        [HttpPost]

        public IActionResult Update(PaymentTypeViewModel pvm)
        {
            try
            {
                string imgUrl = "/Images/";
                PaymentTypeEntity payment = new PaymentTypeEntity()
                {
                    Id = pvm.Id,
                    PaymentType = pvm.PaymentType,
                    PaymentTypeImage = imgUrl+ pvm.PaymentTypeImage,
                    PaymentTypeQR = imgUrl+ pvm.PaymentTypeQR,
                    UpdatedAt = DateTime.Now,
                };
                _paymentTypeService.Update(payment);
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
