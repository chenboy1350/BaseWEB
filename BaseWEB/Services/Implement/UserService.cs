using BaseWEB.Data.Entities;
using BaseWEB.Services.Interface;
using BaseWEB.Services.Helper;
using BaseWEB.Data;
using System.Threading.Tasks;

namespace BaseWEB.Services.Implement
{
    public class UserService(JPDbContext dbContext) : IUserService
    {
        private readonly JPDbContext _dbContext = dbContext;

        public Userid ValidateUser(string username, string password)
        {
            try
            {
                InputValidator validator = new();
                if (validator.IsValidInput(username) && validator.IsValidInput(password))
                {
                    var user = _dbContext.Userid
                        .FirstOrDefault(u => u.Userid1 == username && u.Password == password);

                    return user ?? new Userid();
                }
                else
                {
                    throw new ArgumentException("Invalid input provided.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while validating the user.", ex);
            }
        }
    }
}
