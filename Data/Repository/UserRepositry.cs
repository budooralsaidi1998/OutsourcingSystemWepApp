using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class UserRepositry : IUserRepositry
    {
        private readonly ApplictionDbContext _context;

        // Constructor to inject ApplicationDbContext
        public UserRepositry(ApplictionDbContext context)
        {
            _context = context;
        }

        public int AddUserInt(User user)
        {
            try
            {
                // Add the user to the context and save changes to the database
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception (could be a database error, validation error, etc.)
                Console.WriteLine($"An error occurred while adding the user: {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred while adding the user.", ex);
            }
            return user.UID;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }



        public async Task<int> AddUserIntAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UID;
        }

        public User GetUser(string email, string password)
        {
            try
            {
                // Query the database to find a user matching the provided email and password
                return _context.Users
                    .Where(u => u.Email == email && u.Password == password) // Fixed & to &&
                    .FirstOrDefault(); // Returns the first match or null if no match found
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred while retrieving the user: {ex.Message}");
                // Optionally, rethrow the exception or return null
                throw new Exception("An error occurred while retrieving the user.", ex);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                // Query the database to find users with the specified user ID
                return _context.Users
                    // Filter users by the provided user ID
                    .ToList(); // Convert the result to a list
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred while retrieving users: {ex.Message}");
                // Optionally, rethrow the exception or return an empty list
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }


        public User GetUserByPassword(string password)
        {
            return _context.Users.FirstOrDefault(u => u.Password == password);
        }

        public User GetUserById(int userId)
        {
            return _context.Users
                .Where(u => u.UID == userId)
                .FirstOrDefault();
        }


        public string GetUserEmail(int userid)
        {
            return GetUserById(userid).Email;
        }

        public bool Delete(User user)
        {

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UID == userId);
        }

        public void AddUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);

        public User Login(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public bool DoesEmailExist(string email) => _context.Users.Any(u => u.Email == email);

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public int? GetClientIDByUID(int uid)
        {

            var client = _context.Client.FirstOrDefault(c => c.UID == uid);
            return client?.ClientID;

        }


        public int? GetDeveloperIDByUID(int uid)
        {
            var developer = _context.Developer.FirstOrDefault(d => d.UserID == uid);
            return developer?.DeveloperID;
        }



    }
}
