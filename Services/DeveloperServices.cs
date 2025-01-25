using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Services
{
    public class DeveloperServices : IDeveloperServices
    {

        private readonly IDeveloperRepositry _developerRepositry;
        private readonly IUserRepositry _userRepositry;

        public DeveloperServices(IDeveloperRepositry developerRepositry, IUserRepositry userRepositry)
        {

            _userRepositry = userRepositry;
            _developerRepositry = developerRepositry;

        }
        public void RegisterDeveloper(UserDeveloperInputDto input)
        {
            // Validate the input DTO
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input data cannot be null.");

            // Validate Name
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new ArgumentException("Name is required.");
            if (input.Name.Length > 100)
                throw new ArgumentException("Name cannot exceed 100 characters.");

            // Validate Email
            if (string.IsNullOrWhiteSpace(input.Email))
                throw new ArgumentException("Email is required.");
            if (!new EmailAddressAttribute().IsValid(input.Email))
                throw new ArgumentException("Invalid email format.");

            // Validate Password
            if (string.IsNullOrWhiteSpace(input.Password))
                throw new ArgumentException("Password is required.");
            if (input.Password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            // Validate Role
            if (string.IsNullOrWhiteSpace(input.role))
                throw new ArgumentException("Role is required.");
            var validRoles = new[] { "Developer", "Admin", "Client" };
            if (!validRoles.Contains(input.role))
                throw new ArgumentException("Invalid role. Allowed roles are: Developer, Admin, Client.");

            // Validate Age
            if (input.Age < 18 || input.Age > 120)
                throw new ArgumentException("Age must be between 18 and 120.");

            // Validate Specialization
            if (string.IsNullOrWhiteSpace(input.Specialization))
                throw new ArgumentException("Specialization is required.");

            // Validate Years of Experience
            if (input.YearsOfExperience < 0)
                throw new ArgumentException("Years of experience must be a non-negative number.");

            // Validate Hourly Rate
            if (input.HourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be greater than zero.");

            // Validate Career Summary
            if (!string.IsNullOrEmpty(input.CareerSummary) && input.CareerSummary.Length > 1000)
                throw new ArgumentException("Career summary cannot exceed 1000 characters.");

            // Validate Document Link
            if (!string.IsNullOrEmpty(input.DocumentLink) &&
                !Uri.IsWellFormedUriString(input.DocumentLink, UriKind.Absolute))
                throw new ArgumentException("Invalid URL format for Document Link.");

            // Validate Completed Projects
            //if (input.CompletedProjects < 0)
            //    throw new ArgumentException("Completed projects must be a non-negative number.");

            // Validate CanBePartOfTeam
            //if (input.CanBePartOfTeam != true && input.CanBePartOfTeam != false)
            //    throw new ArgumentException("CanBePartOfTeam is required and must be true or false.");

            // Create and add User entity
            var user = new User
            {
                Name = input.Name,
                Email = input.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                role = input.role,
                CreatedAt = input.CreatedAt,
            };

            int userID = _userRepositry.AddUserInt(user);

            // Create and add Developer entity
            var developer = new Developer
            {
                UserID = userID,
                Age = input.Age,
                Specialization = input.Specialization,
                YearsOfExperience = input.YearsOfExperience,
                HourlyRate = input.HourlyRate,
                DeveloperName = input.DeveloperName,
                // AvailabilityStatus = input.AvailabilityStatus,
                CareerSummary = input.CareerSummary,
                //  CompletedProjects = input.CompletedProjects,
                // CanBePartOfTeam = input.CanBePartOfTeam,
                DocumentLink = input.DocumentLink,
            };

            _developerRepositry.Add(developer);
        }
        public void UpdateDeveloper(int id, UpdateDeveInput updateDeveloper)
        {
            try
            {
                // Ensure the provided updated developer data is not null
                if (updateDeveloper == null)
                    throw new ArgumentNullException(nameof(updateDeveloper), "Updated developer data cannot be null.");

                // Get the existing developer from the repository using ID
                var developer = _developerRepositry.GetUserById(id);

                if (developer == null)
                    throw new KeyNotFoundException($"Developer with ID {id} not found.");

                if (developer.IsDelete == true)
                {
                    throw new Exception("Cannot update a deleted account. Please log out.");
                }

                // Update developer fields only if new values are provided
                if (!string.IsNullOrEmpty(updateDeveloper.DeveloperName))
                    developer.DeveloperName = updateDeveloper.DeveloperName;

                if (updateDeveloper.AvailabilityStatus != null)
                    developer.AvailabilityStatus = updateDeveloper.AvailabilityStatus.Value;

                if (updateDeveloper.Age != null)
                    developer.Age = updateDeveloper.Age.Value;

                if (!string.IsNullOrEmpty(updateDeveloper.Specialization))
                    developer.Specialization = updateDeveloper.Specialization;

                if (updateDeveloper.YearsOfExperience != null)
                    developer.YearsOfExperience = updateDeveloper.YearsOfExperience.Value;

                if (!string.IsNullOrEmpty(updateDeveloper.DocumentLink))
                    developer.DocumentLink = updateDeveloper.DocumentLink;

                if (!string.IsNullOrEmpty(updateDeveloper.CareerSummary))
                    developer.CareerSummary = updateDeveloper.CareerSummary;

                if (updateDeveloper.HourlyRate != null)
                    developer.HourlyRate = updateDeveloper.HourlyRate.Value;

                // Update the last updated timestamp
                developer.UpdateDate = DateTime.Now;

                // Update the developer in the repository
                _developerRepositry.Update(developer);



            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (KeyNotFoundException ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
        public void SoftDeleteClient(int id)
        {
            // Validate the client ID to ensure >1 
            if (id <= 0)
                throw new ArgumentException("developer ID must be greater than zero.");

            try
            {
                // get the client by ID from the repository
                var developer = _developerRepositry.GetUserById(id);

                // Check if the client exists
                if (developer == null)
                    throw new KeyNotFoundException($"developer with ID {id} not found.");

                // Mark the client as soft-deleted in the repository
                _developerRepositry.Delete(developer);


            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while soft-deleting the developer with ID {id}.", ex);
            }
        }

        public IEnumerable<Developer> GetAll()
        {
            return _developerRepositry.GetAll();
        }
        public IEnumerable<filtrationDeveloperdto> GetAlldeveloper(string name, string speclization, decimal? rating, bool? availiabilty, int pageNumber = 1, int pageSize = 10)
        {
            // Validate pagination parameters to ensure they are positive integers

            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            try
            {
                // get all clients from the repository and convert to a query object

                var query = _developerRepositry.GetAll().AsQueryable();

                // Filter by name if a name is provided
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(c => c.DeveloperName.Contains(name, StringComparison.OrdinalIgnoreCase));

                // Filter by industry if an industry is provided
                if (!string.IsNullOrEmpty(speclization))
                    query = query.Where(c => c.Specialization.Contains(speclization));

                // Filter by rating if a rating is provided
                if (rating.HasValue)
                    query = query.Where(c => c.HourlyRate >= rating);

                // Filter by rating if a rating is provided
                if (availiabilty.HasValue)
                    query = query.Where(c => c.AvailabilityStatus == true);


                // Apply pagination

                return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => new filtrationDeveloperdto        // Map the Client entity to ClientDTO
                    {
                        DeveloperName = c.DeveloperName,
                        Specialization = c.Specialization,
                        CommitmentRating = c.CommitmentRating,
                        AvailabilityStatus = c.AvailabilityStatus,
                        CompletedProjects = c.CompletedProjects,


                    })
                    .ToList(); // Execute the query and return the results as a list
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving clients.", ex);
            }
        }
        public IEnumerable<filtrationDeveloperdto> GetName(string name)
        {
            // Validate the industry parameter to ensure it not null 

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("nameof developer cannot be null or empty.");

            try
            {
                // get clients by industry from the repository

                // Map the result to a collection of ClientDTO 
                return _developerRepositry.GetNameDeveloper(name).Select(c => new filtrationDeveloperdto
                {
                    DeveloperName = c.DeveloperName,
                    Specialization = c.Specialization,
                    CommitmentRating = c.CommitmentRating,
                    AvailabilityStatus = c.AvailabilityStatus,
                    CompletedProjects = c.CompletedProjects,

                    // Assign the creation date
                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving developer in the industry '{name}'.", ex);
            }
        }

        public List<DeveloperOutDTO> GetDevsBasedOnSearchValue(string Value)
        {
            var devs = _developerRepositry.GetAll();
            if (Value != null)               
            {
                var value = Value.ToLower();
                devs= devs.Where(d => d.DeveloperName.ToLower().Contains(value) || d.Specialization.ToLower().Contains(value) || d.CareerSummary.ToLower().Contains(value)).ToList();
            }
                var ResultDevs = new List<DeveloperOutDTO>();

                //mapping
                foreach (var dev in devs)
                {
                    var d = new DeveloperOutDTO
                    {
                        DevId= dev.DeveloperID,
                        DeveloperName = dev.DeveloperName,
                        Specialization = dev.Specialization,
                        HourlyRate = dev.HourlyRate,
                        CommitmentRating = dev.CommitmentRating,
                        AvailabilityStatus = dev.AvailabilityStatus,
                        CompletedProjects = dev.CompletedProjects,
                    };
                    ResultDevs.Add(d);
                }
            

            return ResultDevs;
        }

        public IEnumerable<filtrationDeveloperdto> GetSpecilization(string spec)
        {
            // Validate the industry parameter to ensure it not null 

            if (string.IsNullOrEmpty(spec))
                throw new ArgumentException("nameof developer cannot be null or empty.");

            try
            {
                // get clients by industry from the repository

                // Map the result to a collection of ClientDTO 
                return _developerRepositry.GetSpec(spec).Select(c => new filtrationDeveloperdto
                {
                    DeveloperName = c.DeveloperName,
                    Specialization = c.Specialization,
                    CommitmentRating = c.CommitmentRating,
                    AvailabilityStatus = c.AvailabilityStatus,
                    CompletedProjects = c.CompletedProjects,

                    // Assign the creation date
                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving developer in the specilization '{spec}'.", ex);
            }
        }
        public IEnumerable<filtrationDeveloperdto> GetByAvailability(bool av)
        {
            // Validate the industry parameter to ensure it not null 

            if (av == null)
                throw new ArgumentException("nameof developer cannot be null or empty.");

            try
            {
                // get clients by industry from the repository

                // Map the result to a collection of ClientDTO 
                return _developerRepositry.Getavailibilty(av).Select(c => new filtrationDeveloperdto
                {
                    DeveloperName = c.DeveloperName,
                    Specialization = c.Specialization,
                    CommitmentRating = c.CommitmentRating,
                    AvailabilityStatus = c.AvailabilityStatus,
                    CompletedProjects = c.CompletedProjects,

                    // Assign the creation date
                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving developer in the availabilty '{av}'.", ex);
            }
        }

        public IEnumerable<filtrationDeveloperdto> Getrate(decimal rating)
        {
            if (rating == null)

                // Validate the industry parameter to ensure it not null 

                throw new ArgumentException("nameof developer cannot be null or empty.");
            try
            {

                return _developerRepositry.getrate(rating).Select(c => new filtrationDeveloperdto
                {
                    DeveloperName = c.DeveloperName,
                    Specialization = c.Specialization,
                    CommitmentRating = c.CommitmentRating,
                    AvailabilityStatus = c.AvailabilityStatus,



                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving developer in the rating '{rating}'.", ex);
            }

        }

        public Developer GetById(int id)
        {
            try
            {
                return _developerRepositry.GetById(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving the Developer with ID {id}.", ex);
            }
        }
    }
}
