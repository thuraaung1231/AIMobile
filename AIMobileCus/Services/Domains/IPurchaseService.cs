using AIMobile.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public interface IPurchaseService
    {
        void Entry(PurchaseEntity purchase);
    }
}
