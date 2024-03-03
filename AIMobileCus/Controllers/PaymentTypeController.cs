using AIMobile.Models.DataModels;
using AIMobile.Models.ViewModels;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
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
                PaymentTypeEntity payment = new PaymentTypeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    PaymentType = pvm.PaymentType,
                };
                _paymentTypeService.Entry(payment);
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
            IList<PaymentTypeViewModel> payments = _paymentTypeService.ReteriveAll().Select(u => new PaymentTypeViewModel
            {
                Id = u.Id,
                PaymentType = u.PaymentType,
            }).ToList();
            return View(payments);
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
            PaymentTypeViewModel payment = new PaymentTypeViewModel();
            var PaymentDataModel = _paymentTypeService.GetById(id);
            if (PaymentDataModel != null)
            {
                payment.Id = PaymentDataModel.Id;
                payment.PaymentType = PaymentDataModel.PaymentType;
            }
            return View(payment);
        }


        [HttpPost]

        public IActionResult Update(PaymentTypeViewModel pvm)
        {
            try
            {
                PaymentTypeEntity payment = new PaymentTypeEntity()
                {
                    Id = pvm.Id,
                    PaymentType = pvm.PaymentType,
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
