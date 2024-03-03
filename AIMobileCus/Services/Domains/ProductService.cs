using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class ProductService:IProductService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var Product = _applicationDbContext.Product.Find(Id);
            if (Product != null)
            {
                _applicationDbContext.Product.Remove(Product);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(ProductEntity product)
        {
            _applicationDbContext.Product.Add(product);
            _applicationDbContext.SaveChanges();

        }

        public ProductEntity GetById(string id)
        {
            return _applicationDbContext.Product.Where(w => w.Id == id).SingleOrDefault();
        }

        public IList<ProductEntity> ReteriveAll()
        {
            return _applicationDbContext.Product.ToList();
        }

        public void Update(ProductEntity Product)
        {
            _applicationDbContext.Product.Update(Product);
            _applicationDbContext.SaveChanges();
        }
    }
}
