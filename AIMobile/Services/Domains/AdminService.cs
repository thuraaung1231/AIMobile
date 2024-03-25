using AIMobile.DAO;
using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Entry(AdminRegisterEntity adminRegister)
        {
            _applicationDbContext.Admin.Add(adminRegister);
            _applicationDbContext.SaveChanges();
        }

        public AdminRegisterEntity GetByEmail(string Email)
        {
            return _applicationDbContext.Admin.Where(a => a.EmailAddress == Email).FirstOrDefault();
        }

        public IList<AdminRegisterEntity> RetrieveAll()
        {
            return _applicationDbContext.Admin.ToList();
        }

        public AdminRegisterEntity RetrieveByEmailAndPassword(string Email, string EnterPassword)
        {
            return _applicationDbContext.Admin.Where(a => a.EmailAddress == Email && a.Password == EnterPassword).FirstOrDefault();
        }
    }
}
