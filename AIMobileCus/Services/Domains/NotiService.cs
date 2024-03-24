using AIMobile.DAO;
using AIMobileCus.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public class NotiService : INotiService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NotiService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Entry(NotiEntity notiEntity)
        {
            _applicationDbContext.Noti.Add(notiEntity);
            _applicationDbContext.SaveChanges();
        }

        public IList<NotiEntity> RetrieveAll(string customerId)
        {
            return _applicationDbContext.Noti.Where(n=>n.CustomerId == customerId).ToList();    
        }
    }
}
