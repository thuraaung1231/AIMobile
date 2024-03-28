using AIMobile.DAO;
using AIMobileCus.Models.DataModels;
using System.Drawing.Drawing2D;

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
        public NotiEntity GetById(string id)
        {
            return _applicationDbContext.Noti.Where(w => w.Id == id).SingleOrDefault();
        }


        public IList<NotiEntity> RetrieveAll()
        {
            return _applicationDbContext.Noti.ToList();
        }

        public void Update(NotiEntity Noti)
        {
       
            _applicationDbContext.Noti.Update(Noti);
            _applicationDbContext.SaveChanges();
        }
    }
}
