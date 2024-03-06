using AIMobile.DAO;
using AIMobile.Models.DataModels;
using System.Drawing.Drawing2D;

namespace AIMobile.Services.Domains
{
    public class TypeServices : ITypeServices
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TypeServices(ApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var Type = _applicationDbContext.Type.Find(Id);
            if (Type != null)
            {
                _applicationDbContext.Type.Remove(Type);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(TypeEntity type)
        {
            _applicationDbContext.Type.Add(type);
            _applicationDbContext.SaveChanges();
        }

        public TypeEntity GetById(string id)
        {
            return _applicationDbContext.Type.Where(w => w.Id == id).SingleOrDefault();
        }
        public TypeEntity GetByName(string Name)
        {
            return _applicationDbContext.Type.Where(w => w.Name == Name).SingleOrDefault();
        }

        public IList<TypeEntity> ReteriveAll()
        {
            return _applicationDbContext.Type.ToList();
        }

        public void Update(TypeEntity Type)
        {
            _applicationDbContext.Type.Update(Type);
            _applicationDbContext.SaveChanges();
        }
    }
}
