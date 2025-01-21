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

        public void AddUser(User user)
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
        }
        //to get the id user
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
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
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

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
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
        public bool DoesEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UID == userId);
        }
    }
}
