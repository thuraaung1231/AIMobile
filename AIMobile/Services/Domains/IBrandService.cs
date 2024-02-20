using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IBrandService
    {
        void Entry(BrandEntity brand);
        IList<BrandEntity> ReteriveAll();
        void Update(BrandEntity brand);
        BrandEntity GetById(int id);
        void Delete(string Id);
    }
}
