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

        public BrandEntity GetById(int id)
        {
            return _applicationDbContext.Brand.Find(id);
        }

        public IList<BrandEntity> ReteriveAll()
        {
       IList<BrandEntity> brands = _applicationDbContext.Brand.ToList();
            return brands;
        }

        public void Update(BrandEntity brand)
        {
            _applicationDbContext.Brand.Update(brand);
            _applicationDbContext.SaveChanges();
        }
    }
}
