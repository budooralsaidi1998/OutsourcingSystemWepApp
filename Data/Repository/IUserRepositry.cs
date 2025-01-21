using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IUserRepositry
    {
        void AddUser(User user);
        int AddUserInt(User user);
        bool Delete(User user);
        bool DoesEmailExist(string email);
        List<User> GetAllUsers();
        User GetUser(string email, string password);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserByPassword(string password);
        string GetUserEmail(int userid);
        void Update(User user);
        bool UserExists(int userId);
    }
}