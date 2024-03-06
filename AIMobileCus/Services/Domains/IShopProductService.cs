using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IShopProductService
    {
        void Entry(ShopProductEntity shopProduct);
        
        IList<ShopProductEntity> ReteriveAll();
        void Update(ShopProductEntity shopProduct);
        ShopProductEntity GetById(string Id);
        void Delete(string Id);
        ShopProductEntity GetShopProduct(string ProductId, string ImageId);
        ShopProductEntity GetShopProductByProductId(string productId);
    }
}
