using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public interface IUserService
    {
        void Entry(UserEntity user);
        IList<UserEntity> ReteriveAll();
        void Update(UserEntity user);
        UserEntity GetById(string id);
        void Delete(string Id);
    }
}
