using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IPaymentTypeService
    {
        void Entry(PaymentTypeEntity payment);
        IList<PaymentTypeEntity> ReteriveAll();
        void Update(PaymentTypeEntity payment);
        PaymentTypeEntity GetById(string id);
        void Delete(string Id);
    }
}
