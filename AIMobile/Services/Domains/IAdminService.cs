using AIMobile.Models.DataModels;

namespace AIMobile.Services.Domains
{
    
    public interface IAdminService
    {
        
        void Entry(AdminRegisterEntity adminRegister);
        IList<AdminRegisterEntity> RetrieveAll();
        AdminRegisterEntity RetrieveByEmailAndPassword(string Email,string EnterPassword);
        AdminRegisterEntity GetByEmail(string Email);
    }
}
