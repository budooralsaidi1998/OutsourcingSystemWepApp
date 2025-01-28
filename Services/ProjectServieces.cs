using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Services
{
    public class ProjectServieces : IProjectServieces
    {
        private readonly IProjectRepositry _projectRepositry;


        public ProjectServieces(IProjectRepositry projectRepositry)
        {

            _projectRepositry = projectRepositry;

        }

        public void AddProject(int idclienttoken, ProjectRequestInputDto project)
        {

            var projectIn = new Project
            {
                ClientID = idclienttoken,
                DeveloperID = project.Developerid ?? 0,
                Description = project.Description,
                Name = project.Name,
                TeamID = project.Teamid ?? 0,
                StartAt = project.StartAt,
                EndAt = project.EndAt,
                //Status = project.Status,
                DailyHoursNeeded = project.DailyHoursNeeded,

            };

            _projectRepositry.Addprojetc(projectIn);
        }

        public Project GetProjectById(int id)
        {
            try
            {
                if (id == null)

                    throw new UnauthorizedAccessException("it null id  .");
                var project = _projectRepositry.GetProjectByID(id);
                if (project == null)
                {
                    throw new UnauthorizedAccessException("it null project  .");
                }

                return project;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }

        }
        public List<Project> GetProjectsByDevID(int DevID)
        {
            try
            {
                if (DevID == null)

                    throw new UnauthorizedAccessException("it null id  .");

                var projects = _projectRepositry.GetProjectsByDevID(DevID);
                if (projects == null)
                {
                    throw new UnauthorizedAccessException("it null project  .");
                }

                return projects;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }


        }

        public List<Project> GetProjectsByTeamID(int TeamID)
        {
            try
            {
                if (TeamID == null)

                    throw new UnauthorizedAccessException("it null id  .");

                var projects = _projectRepositry.GetProjectsByTeamID(TeamID);
                if (projects == null)
                {
                    throw new UnauthorizedAccessException("it null project  .");
                }

                return projects;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }


        }
        public Project GetProjectByClientId(int id)
        {
            try
            {
                if (id == null)

                    throw new UnauthorizedAccessException("it null id  .");
                var project = _projectRepositry.GetProjectByIDClient(id);
                if (project == null)
                {
                    throw new UnauthorizedAccessException("it null project  .");
                }

                return project;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }


        }

        public void UpdateProject(int id, UpdateProjectDto project)
        {
            try
            {
                // Ensure the provided updated project data is not null
                if (project == null)
                    throw new ArgumentNullException(nameof(project), "Updated project data cannot be null.");

                // Validate the project DTO against its data annotations
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(project, null, null);

                if (!Validator.TryValidateObject(project, validationContext, validationResults, true))
                {
                    // Collect validation errors and throw an exception with details
                    string errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    throw new ArgumentException($"Validation failed: {errors}");
                }

                // Get the existing project from the repository using ID
                Project projectt = _projectRepositry.GetProjectByIDClient(id);

                if (projectt == null)
                    throw new KeyNotFoundException($"Project with ID {id} not found.");

                if (projectt.IsDelete == true)
                {
                    throw new Exception("Cannot update a deleted project. Please log out.");
                }

                // Update project fields only if new values are provided
                if (!string.IsNullOrEmpty(project.Name))
                {
                    projectt.Name = project.Name;
                }
                if (!string.IsNullOrEmpty(project.Description))
                {
                    projectt.Description = project.Description;
                }
                if (project.StartAt != default(DateTime))
                {
                    projectt.StartAt = project.StartAt;
                }
                if (project.EndAt.HasValue)
                {
                    projectt.EndAt = project.EndAt.Value;
                }

                // Update the last updated timestamp
                projectt.UpdateDate = DateTime.Now;

                // Update the project in the repository
                _projectRepositry.UpdateProject(projectt);
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

        public bool SoftDeleteClient(int id)
        {
            // Validate the client ID to ensure >1 
            if (id <= 0)
                throw new ArgumentException("project ID must be greater than zero.");

            try
            {
                // get the client by ID from the repository
                var project = _projectRepositry.GetProjectByIDClient(id);

                // Check if the client exists
                if (project == null)
                    throw new KeyNotFoundException($"project with ID {id} not found.");

                // Mark the client as soft-deleted in the repository
                project.IsDelete = true;
                _projectRepositry.UpdateProject(project);
                return _projectRepositry.DeleteProject(project);


            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while soft-deleting the project with ID {id}.", ex);
            }
        }

    }
}
