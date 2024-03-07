using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
   
    public class ShopProductService : IShopProductService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShopProductService(ApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var ShopProduct = _applicationDbContext.ShopProduct.Find(Id);
            if (ShopProduct != null)
            {
                _applicationDbContext.ShopProduct.Remove(ShopProduct);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(ShopProductEntity shopProduct)
        {
            _applicationDbContext.ShopProduct.Add(shopProduct);
            _applicationDbContext.SaveChanges();
        }

        public ShopProductEntity GetById(string Id)
        {
            return _applicationDbContext.ShopProduct.Find(Id);
        }
        public ShopProductEntity GetShopProduct(string productId,string imageId)
        {
            return _applicationDbContext.ShopProduct.Where(p=>p.ProductId==productId && p.ImageId==imageId).FirstOrDefault();
        }
        public IList<ShopProductEntity> ReteriveAll()
        {
            return _applicationDbContext.ShopProduct.ToList();
        }
        public ShopProductEntity GetShopProductByProductId(string productId)
        {
            return _applicationDbContext.ShopProduct.Where(s => s.ProductId == productId).FirstOrDefault();
        }
        public void Update(ShopProductEntity shopProduct)
        {
            _applicationDbContext.ShopProduct.Update(shopProduct);
            _applicationDbContext.SaveChanges();
        }
    }
}
