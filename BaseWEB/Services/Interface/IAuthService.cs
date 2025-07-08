using BaseWEB.Models;
using static BaseWEB.Services.Implement.AuthService;

namespace BaseWEB.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResult> LoginUserAsync(string username, string password, bool rememberMe);
        Task<RefreshTokenResult> RefreshTokenAsync();
        Task<bool> LogoutAsync();
    }
}
