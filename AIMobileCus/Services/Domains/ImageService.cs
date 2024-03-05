using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class ImageService:IImageService
    {
        private readonly ApplicationDbContext _application;
        public ImageService(ApplicationDbContext application)
        {
            _application = application; 
        }
        public void Delete(string Id)
        {
            var image = _application.Image.Find(Id);
            if(image != null) 
            {
                _application.Image.Remove(image);
                _application.SaveChanges();
            }
        }
        public void Entry(ImageEntity image)
        {
            _application.Image.Add(image);
            _application.SaveChanges();
        }
        public ImageEntity GetById(string id)
        {
            return _application.Image.Where(w => w.Id == id).SingleOrDefault();
        }
        public IList<ImageEntity> ReteriveAll()
        {
            return _application.Image.ToList();
        }
        public void Update(ImageEntity image) 
        {
            _application.Image.Update(image);
            _application.SaveChanges();
        }
    }
}
