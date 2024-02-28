using AIMobile.Models.DataModels;
namespace AIMobile.Services.Domains
{
    public interface IImageService
    {
        void Entry(ImageEntity image);
        IList<ImageEntity> ReteriveAll();
        void Update(ImageEntity image);
        ImageEntity GetById(string id);
        void Delete(string Id);
    }
}
