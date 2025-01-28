using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using System.Security.Claims;

namespace OutsourcingSystemWepApp.Services
{
    public interface IUserServices
    {
        int AddUserAdmin(AdminInputDto user);
        bool DeleteUser(int userIdFromToken);
        List<User> GetAllUsers(ClaimsPrincipal user);
        string GetEmail(int userid);
        User GetUserByID(int ID, ClaimsPrincipal user);
        User Login(string email, string password);
        bool UpdateUser(int userIdFromToken, UserUpdateDto updateRequest);
        bool UserExists(int userId);
        User GetUserByEmail(string Email);
    }
}