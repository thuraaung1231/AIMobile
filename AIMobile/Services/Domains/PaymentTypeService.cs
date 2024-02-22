using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PaymentTypeService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        void IPaymentTypeService.Delete(string Id)
        {
            var payment = _applicationDbContext.Payment.Find(Id);
            if (payment != null)
            {
                _applicationDbContext.Payment.Remove(payment);
                _applicationDbContext.SaveChanges();
            }
        }

        void IPaymentTypeService.Entry(PaymentTypeEntity payment)
        {
            _applicationDbContext.Payment.Add(payment);
            _applicationDbContext.SaveChanges();
        }

        PaymentTypeEntity IPaymentTypeService.GetById(string id)
        {
            return _applicationDbContext.Payment.Find(id);
        }

        IList<PaymentTypeEntity> IPaymentTypeService.ReteriveAll()
        {
            return _applicationDbContext.Payment.ToList();
        }

        void IPaymentTypeService.Update(PaymentTypeEntity payment)
        {
            _applicationDbContext.Payment.Update(payment);
            _applicationDbContext.SaveChanges();
        }
    }
}
