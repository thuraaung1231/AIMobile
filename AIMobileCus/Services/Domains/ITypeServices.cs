using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface ITypeServices
    {
        void Entry(TypeEntity type);
        IList<TypeEntity> ReteriveAll();
        void Update(TypeEntity Type);
        TypeEntity GetById(string id);
        void Delete(string Id);
        public TypeEntity GetByName(string Name);
    }
}
