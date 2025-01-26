using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;
using System.Security.Claims;

namespace OutsourcingSystemWepApp.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositry _userrepo;
        private readonly IClientRepository _clientRepository;
        private readonly IDeveloperRepositry _developerrepo;
        // Constructor to inject the IUserRepo dependency
        public UserServices(IUserRepositry userrepo, IClientRepository clientRepository, IDeveloperRepositry developerrepo)
        {
            // Assigning the injected IUserRepo instance to the private field
            _userrepo = userrepo;
            _clientRepository = clientRepository;
            _developerrepo = developerrepo;
        }




        public bool UserExists(int userId)
        {
            return _userrepo.UserExists(userId);
        }




        public User GetUserByID(int ID, ClaimsPrincipal user)
        {
            try
            {
                var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    throw new UnauthorizedAccessException("Only admin users can get user details by ID.");
                }

                return _userrepo.GetUserById(ID);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                throw new UnauthorizedAccessException($"Error getting user by ID: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }
        }

        public bool DeleteUser(int userIdFromToken)
        {
            try
            {
                //if (role == "Admin")
                //{
                var userToDelete = _userrepo.GetUserById(userIdFromToken);
                if (userToDelete == null)
                    throw new Exception("User not found.");

                userToDelete.IsDeleted = true;
                _userrepo.Update(userToDelete);
                return true;
                //}
                //throw new UnauthorizedAccessException("You do not have permission to delete this user.");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions
                throw new UnauthorizedAccessException($"Error deleting user: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                throw new Exception($"An unexpected error occurred while deleting the user: {ex.Message}");
            }
        }



        //new 


        public int AddUserAdmin(AdminInputDto user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    throw new ArgumentException("The name is required.");
                }

                if (_userrepo.DoesEmailExist(user.Email))
                {
                    throw new ArgumentException("A user with this email already exists.");
                }

                var validRoles = new[] { "Developer", "Admin", "Client" };
                if (!validRoles.Contains(user.role))
                {
                    throw new ArgumentException("Invalid role. Must be Admin, Client, or Developer.");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var completeUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    role = user.role,
                    CreatedAt = user.CreatedAt,
                };

                _userrepo.AddUser(completeUser);
                return completeUser.UID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user: {ex.Message}");
            }
        }

        public User Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Email and password are required.");
                }

                var user = _userrepo.GetUserByEmail(email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }

        public List<User> GetAllUsers(ClaimsPrincipal user)
        {
            try
            {
                if (!user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                {
                    throw new UnauthorizedAccessException("Only admin can get all users.");
                }
                return _userrepo.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all users: {ex.Message}");
            }
        }

        public bool UpdateUser(int userIdFromToken, UserUpdateDto updateRequest)
        {
            try
            {
                var userToUpdate = _userrepo.GetUserById(userIdFromToken);
                if (userToUpdate == null || userToUpdate.IsDeleted)
                {
                    throw new Exception("Cannot update a deleted or non-existent user.");
                }

                bool isUpdated = false;

                if (!string.IsNullOrEmpty(updateRequest.Name))
                {
                    userToUpdate.Name = updateRequest.Name;
                    isUpdated = true;
                }

                if (!string.IsNullOrEmpty(updateRequest.Email))
                {
                    userToUpdate.Email = updateRequest.Email;
                    isUpdated = true;
                }

                if (!isUpdated)
                {
                    throw new Exception("No update fields provided.");
                }

                userToUpdate.UpdatedAt = DateTime.Now;
                _userrepo.Update(userToUpdate);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public string GetEmail(int userid)
        {
            return _userrepo.GetUserEmail(userid);
        }
    }
}
