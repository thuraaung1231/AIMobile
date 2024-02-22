using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IShopService
    {
        void Entry(ShopEntity shop);
        IList<ShopEntity> ReteriveAll();
        void Update(ShopEntity shop);
        ShopEntity GetById(string id);
        void Delete(string Id);
    }
}
