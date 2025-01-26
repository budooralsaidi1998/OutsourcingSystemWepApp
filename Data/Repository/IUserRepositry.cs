using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IUserRepositry
    {
        void AddUser(User user);
        int AddUserInt(User user);
        Task<int> AddUserIntAsync(User user);
        bool Delete(User user);
        bool DoesEmailExist(string email);
        List<User> GetAllUsers();
        int? GetClientIDByUID(int uid);
        int? GetDeveloperIDByUID(int uid);
        User GetUser(string email, string password);
        User GetUserByEmail(string email);
        Task<User?> GetUserByEmailAsync(string email);
        User GetUserById(int userId);
        User GetUserByPassword(string password);
        string GetUserEmail(int userid);
        User Login(string email, string password);
        void Update(User user);
        bool UserExists(int userId);
    }
}