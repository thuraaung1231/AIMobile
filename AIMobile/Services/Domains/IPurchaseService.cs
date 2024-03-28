using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IPurchaseService
    {

        void Entry(PurchaseEntity purchase);

        IList<PurchaseEntity> ReteriveAll();
        PurchaseEntity GetById(string id);
        void Delete(string Id);
        IList<PurchaseEntity> GetByShopProductAndDate(string shopProductId, DateTime fromDate,DateTime toDate);
    }
}
