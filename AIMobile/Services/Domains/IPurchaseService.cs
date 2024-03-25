using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IPurchaseService
    {
        void Entry(PurchaseEntity purchase);
    }
}
