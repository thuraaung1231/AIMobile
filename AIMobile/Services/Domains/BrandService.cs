using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrandService(ApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var brand = _applicationDbContext.Brand.Find(Id);
            if (brand != null)
            {
                _applicationDbContext.Brand.Remove(brand);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(BrandEntity brand)
        {
           _applicationDbContext.Brand.Add(brand);
            _applicationDbContext.SaveChanges();

        }

        public BrandEntity GetById(string id)
        {
            return _applicationDbContext.Brand.Where(w => w.Id == id).SingleOrDefault();
        }

        public IList<BrandEntity> ReteriveAll()
        {
            return _applicationDbContext.Brand.ToList();
        }

        public void Update(BrandEntity brand)
        {
            _applicationDbContext.Brand.Update(brand);
            _applicationDbContext.SaveChanges();
        }
       
    }
}
