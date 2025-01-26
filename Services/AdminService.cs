using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepositry _userRepository;

        public AdminService(IUserRepositry userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAdmin(AdminRegisterDto adminDto)
        {
            if (string.IsNullOrWhiteSpace(adminDto.Name) || string.IsNullOrWhiteSpace(adminDto.Email))
            {
                throw new ArgumentException("Name and Email are required fields.");
            }

            // Check if the email already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(adminDto.Email.ToLower());
            if (existingUser != null)
            {
                throw new ArgumentException("This email is already registered.");
            }

            // Create a new user
            var user = new User
            {
                Name = adminDto.Name,
                Email = adminDto.Email.ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password), // Hash password
                role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            // Add user to database
            int userId = await _userRepository.AddUserIntAsync(user);

            return userId > 0;
        }
    }
}
