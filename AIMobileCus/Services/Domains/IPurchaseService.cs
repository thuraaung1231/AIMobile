using AIMobile.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public interface IPurchaseService
    {
        void Create(PurchaseEntity purchase);
        IList<PurchaseEntity> RetrieveAll();
        PurchaseEntity GetById(int id); 
    }
}
