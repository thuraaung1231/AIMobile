using AIMobile.DAO;
using AIMobile.Models.DataModels;

using System.Security.Policy;


namespace AIMobile.Services.Domains
{
    public class PurchaseService : IPurchaseService
    {

        private readonly ApplicationDbContext _application;

        public PurchaseService(ApplicationDbContext application)
        {
            _application = application;
        }
        public void Entry(PurchaseEntity purchase)
        {
         _application.Purchase.Add(purchase);  

        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var Purchase = _applicationDbContext.Purchase.Find(Id);
            if (Purchase != null)
            {
                _applicationDbContext.Purchase.Remove(Purchase);
                _applicationDbContext.SaveChanges();
            }
        }

        public PurchaseEntity GetById(string id)
        {
            return _applicationDbContext.Purchase.Where(w => w.Id == id).SingleOrDefault();
        }

        public IList<PurchaseEntity> ReteriveAll()
        {
            return _applicationDbContext.Purchase.ToList();

        }
    }
}
