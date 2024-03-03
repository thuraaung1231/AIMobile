using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IProductService
    {
        void Entry(ProductEntity product);
        IList<ProductEntity> ReteriveAll();
        void Update(ProductEntity product);
        ProductEntity GetById(string id);
        void Delete(string Id);
    }
}
