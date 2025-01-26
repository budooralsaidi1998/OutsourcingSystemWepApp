using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.helpers;

namespace OutsourcingSystemWepApp.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        Task<string> Login(string email, string password);
    }
}