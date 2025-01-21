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

        public int AddUserAdmin(AdminInputDto user)
        {
            try
            {
                // Validate the user's name
                if (user.Name == null)
                {
                    throw new ArgumentException("The name is required.");
                }

                // Check if a user with the same email already exists
                var existingUserByEmail = _userrepo.GetUserByEmail(user.Email);
                if (existingUserByEmail != null)
                {
                    throw new ArgumentException("A user with this email already exists.");
                }

                // Check for duplicate password
                var existingUserByPassword = _userrepo.GetUserByPassword(user.Password);
                if (existingUserByPassword != null)
                {
                    throw new ArgumentException("A user with this password already exists. Please choose a different password.");
                }
                //validate role 
                var validRoles = new[] { "Developer", "Admin", "Client" };
                if (string.IsNullOrWhiteSpace(user.role) || !validRoles.Contains(user.role))
                {
                    throw new ArgumentException("The role must be one of the following: Developer, Admin, Client.");
                }

                // Hash the password
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Create a new user object
                var completeUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    role = user.role,
                    CreatedAt = user.CreatedAt,
                };

                // Calls the AddUser method of the IUserRepo implementation
                _userrepo.AddUser(completeUser);
                return completeUser.UID;
            }
            catch (ArgumentException ex)
            {
                // Handle specific argument exceptions
                throw new ArgumentException($"Error adding user: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }
        }


        public bool UserExists(int userId)
        {
            return _userrepo.UserExists(userId);
        }


        //public void ApproveClient(ApprovalDto approval, ClaimsPrincipal user, int userid)
        //{
        //    var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

        //    if (!isAdmin)
        //    {
        //        throw new UnauthorizedAccessException("Only admin can approve");
        //    }

        //    var client = _clientRepository.GetById(approval.ClientId);
        //    if (client == null)
        //    {
        //        throw new ArgumentException("Client not found.");
        //    }

        //    client.IsApprove = approval.IsApprove;
        //    client.ApproveBy = userid;

        //    //client.ApprovedByAdmin = approval.ApprovedByAdmin; // Optional: Add ApprovedByAdmin in the Client class.
        //    _clientRepository.Update(client);
        //}


        //public IEnumerable<Client> GetUnapprovedClients(ClaimsPrincipal user)
        //{
        //    var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        //    if (!isAdmin)
        //    {
        //        throw new UnauthorizedAccessException("Only admin can Get the User Approve ");
        //    }
        //    return _clientRepository.GetUnapprovedClients();
        //}



        //public void Approvedeveloper(ApproveDeveloper approval, ClaimsPrincipal user, int userid)
        //{
        //    var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

        //    if (!isAdmin)
        //    {
        //        throw new UnauthorizedAccessException("Only admin can approve");
        //    }

        //    var developer = _developerrepo.GetById(approval.DeveloperId);
        //    if (developer == null)
        //    {
        //        throw new ArgumentException("developer not found.");
        //    }

        //    developer.IsApprove = approval.IsApprove;
        //    developer.IsApproveBy = userid;


        //    _developerrepo.Update( developer);
        //}




        //public IEnumerable<Developer> GetUnapprovedDeveloper(ClaimsPrincipal user)
        //{
        //    var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        //    if (!isAdmin)
        //    {
        //        throw new UnauthorizedAccessException("Only admin can Get the User Approve ");
        //    }
        //    return _developerrepo.GetUnapproveddeveloper();
        //}


        public string GetEmail(int userid)
        {
            return _userrepo.GetUserEmail(userid);
        }



        public User Login(string email, string password)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Email, password, and role are required.");
                }

                var user = _userrepo.GetUserByEmail(email);
                if (user == null)
                {
                    throw new ArgumentException("Invalid email or password.");
                }

                if (user.IsDeleted == true)
                {
                    throw new ArgumentException("This account is not activated.");
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                //if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                //{
                //    var admin = _userrepo.GetUserById(user.UID);
                //    if (admin != null && admin.IsDeleted)
                //    {
                //        throw new InvalidOperationException("You can't login.");
                //    }
                //}
                //else if (role.Equals("Client", StringComparison.OrdinalIgnoreCase))
                //{
                //    var client = _clientRepository.GetByuid(user.UID);
                //    if (client == null || !client.IsApprove)
                //    {
                //        throw new InvalidOperationException("Your account has not been approved by an admin yet.");
                //    }
                //}
                //else if (role.Equals("Developer", StringComparison.OrdinalIgnoreCase))
                //{
                //    var developer = _developerrepo.GetById(user.UID);
                //    if (developer == null || !developer.IsApprove)
                //    {
                //        throw new InvalidOperationException("Your account has not been approved by an admin yet.");
                //    }
                //}
                //else
                //{
                //    throw new ArgumentException("Invalid role.");
                //}

                return user;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error during login: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Login failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error in Login method: {ex.Message}", ex);
            }
        }





        public List<User> GetAllUsers(ClaimsPrincipal user)
        {
            try
            {
                var isAdmin = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    throw new UnauthorizedAccessException("Only admin can Get all User ");
                }
                // Fetch all users from the repository
                return _userrepo.GetAllUsers();
            }
            catch (Exception ex)
            {
                // Handle any errors related to fetching users
                throw new Exception($"Error retrieving all users: {ex.Message}");
            }
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

        public bool UpdateUser(int userIdFromToken, UserUpdateDto updateRequest)
        {
            try
            {

                var userToUpdate = _userrepo.GetUserById(userIdFromToken);

                if (userToUpdate == null)
                {
                    throw new Exception("User not found.");
                }

                if (userToUpdate.IsDeleted == true)
                {
                    throw new Exception("Cannot update a deleted account. Please log out.");
                }

                // Validate if any update fields are provided
                bool isUpdated = false;

                // Update only allowed fields
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

                // If no updates are provided, throw an error
                if (!isUpdated)
                {
                    throw new Exception("No update fields provided. Please provide at least one field to update.");
                }

                // Update the timestamp
                userToUpdate.UpdatedAt = DateTime.Now;

                // Save changes
                _userrepo.Update(userToUpdate);
                return true;
            }
            catch (Exception ex)
            {
                // Handle any errors related to updating the user
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

    }
}
