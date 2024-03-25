using AIMobile.DAO;
using AIMobile.Models.DataModels;

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
        }
    }
}
