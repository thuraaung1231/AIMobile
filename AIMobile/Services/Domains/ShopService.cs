using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class ShopService : IShopService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShopService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var shop = _applicationDbContext.Shop.Find(Id);
            if (shop != null)
            {
                _applicationDbContext.Shop.Remove(shop);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(ShopEntity shop)
        {
            _applicationDbContext.Shop.Add(shop);
            _applicationDbContext.SaveChanges();
        }

        public ShopEntity GetById(string id)
        {
            return _applicationDbContext.Shop.Find(id);
        }

        public IList<ShopEntity> ReteriveAll()
        {
            return _applicationDbContext.Shop.ToList();
        }

        public void Update(ShopEntity shop)
        {
            _applicationDbContext.Shop.Update(shop);
            _applicationDbContext.SaveChanges();
        }
    }
}
