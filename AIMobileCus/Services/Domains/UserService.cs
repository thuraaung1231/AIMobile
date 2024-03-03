using AIMobile.DAO;
using AIMobile.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace AIMobile.Services.Domains
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Delete(string Id)
        {
            var user = _applicationDbContext.User.Find(Id);
            if (user != null)
            {
                _applicationDbContext.User.Remove(user);
                _applicationDbContext.SaveChanges();
            }
        }

        public void Entry(UserEntity user)
        {
            _applicationDbContext.User.Add(user);
            _applicationDbContext.SaveChanges();
        }

        public UserEntity GetById(string id)
        {
            return _applicationDbContext.User.Find(id);
        }

        public IList<UserEntity> ReteriveAll()
        {
            return _applicationDbContext.User.ToList();
        }

        public void Update(UserEntity user)
        {
            _applicationDbContext.User.Update(user);
            _applicationDbContext.SaveChanges();
        }
    }
}
