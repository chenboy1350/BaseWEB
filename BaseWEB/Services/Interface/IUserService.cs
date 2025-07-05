using BaseWEB.Data.Entities;

namespace BaseWEB.Services.Interface
{
    public interface IUserService
    {
        Userid ValidateUser(string username, string password);
    }
}
