using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _dbContext;
        public PurchaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(PurchaseEntity purchase)
        {
            _dbContext.Purchase.Add(purchase);
            _dbContext.SaveChanges();
        }

        public PurchaseEntity GetById(int id)
        {
            return _dbContext.Purchase.Find(id);
        }

        public IList<PurchaseEntity> RetrieveAll()
        {
            return _dbContext.Purchase.ToList();
        }
    }
}
