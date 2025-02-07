﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Services
{
    public class DeveloperServices : IDeveloperServices
    {
        private readonly ApplictionDbContext _context;
        private readonly IDeveloperRepositry _developerRepositry;
        private readonly IUserRepositry _userRepositry;

        public DeveloperServices(ApplictionDbContext context, IDeveloperRepositry developerRepositry, IUserRepositry userRepositry)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepositry = userRepositry;
            _developerRepositry = developerRepositry;

        }



        public async Task<bool> RegisterDeveloper(RegisterDeveloperDto developerDto)
        {
            if (await _userRepositry.GetUserByEmailAsync(developerDto.Email.ToLower()) != null)
            {
                throw new Exception("This email is already registered.");
            }

            var user = new User
            {
                Name = developerDto.Name,
                Email = developerDto.Email.ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(developerDto.Password),
                role = "Developer",
                CreatedAt = DateTime.UtcNow
            };

            int userId = await _userRepositry.AddUserIntAsync(user);

            var developer = new Developer
            {
                UserID = userId,
                Specialization = developerDto.Specialization,
                YearsOfExperience = developerDto.YearsOfExperience,
                Age = developerDto.Age,
                HourlyRate = developerDto.HourlyRate,
                DeveloperName = developerDto.Name,
                CareerSummary = developerDto.CareerSummary ?? "",
                DocumentLink = developerDto.DocumentLink ?? "",
                AvailabilityStatus = true,
                CompletedProjects = 0,
                CanBePartOfTeam = true,
                IsDelete = false,
                UpdateDate = DateTime.Now,
                CommitmentRating = 0
            };

            await _developerRepositry.AddAsync(developer);
            return true;
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
                developer.IsDelete = true;

                _developerRepositry.Update(developer);

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
                        DeveloperId=c.UserID,
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
                devs = devs.Where(d => d.DeveloperName.ToLower().Contains(value) || d.Specialization.ToLower().Contains(value) || d.CareerSummary.ToLower().Contains(value)).ToList();
            }
            var ResultDevs = new List<DeveloperOutDTO>();

            //mapping
            foreach (var dev in devs)
            {
                var d = new DeveloperOutDTO
                {
                    DevId = dev.DeveloperID,
                    imagePath = dev.imagePath,
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


        //new 








        public async Task UpdateDeveloperProfile(DeveloperDTOForProfile developerDto)
        {
            var developer = await _context.Developer.FirstOrDefaultAsync(d => d.DeveloperID == developerDto.DeveloperID);
            if (developer == null)
                throw new Exception("Developer not found");

            developer.Specialization = developerDto.Specialization;
            developer.YearsOfExperience = developerDto.YearsOfExperience;
            developer.Age = developerDto.Age;
            developer.HourlyRate = developerDto.HourlyRate;
            developer.CareerSummary = developerDto.CareerSummary;
            developer.CompletedProjects = developerDto.CompletedProjects;
            // developer.CurrentProject = developerDto.CurrentProject;
            developer.DocumentLink = developerDto.DocumentLink;
            developer.imagePath = developerDto.imagePath;
            _developerRepositry.Update(developer);
           
        }

        



        public async Task<string> UploadDocument(IBrowserFile file)
        {
            var filePath = Path.Combine("wwwroot/uploads", file.Name);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(stream);

            return $"/uploads/{file.Name}";
        }


        //new for skill 
        public async Task<Developer> GetDeveloperByID(int developerID)
        {
            return await _context.Developer
                .FirstOrDefaultAsync(d => d.DeveloperID == developerID);
        }



        //for image

        public async Task<DeveloperDTOForProfile> GetDeveloperProfile(int developerId)
        {
            var developer = await _context.Developer
                .Include(d => d.User)
                .Where(d => d.DeveloperID == developerId)
                .Select(d => new DeveloperDTOForProfile
                {
                    DeveloperID = d.DeveloperID,
                    Name = d.User.Name,
                    Email = d.User.Email,
                    Specialization = d.Specialization,
                    YearsOfExperience = d.YearsOfExperience,
                    Age = d.Age,
                    HourlyRate = d.HourlyRate,
                    CareerSummary = d.CareerSummary,
                    CompletedProjects = d.CompletedProjects,
                    imagePath = d.imagePath,
                    DocumentLink = d.DocumentLink
                })
                .FirstOrDefaultAsync();

            return developer ?? throw new Exception("Developer not found");
        }


        public async Task<bool> UpdateDeveloperImage(int id, string imagePath)
        {
            return await _developerRepositry.UpdateDeveloperImage(id, imagePath);
        }




    }

}

