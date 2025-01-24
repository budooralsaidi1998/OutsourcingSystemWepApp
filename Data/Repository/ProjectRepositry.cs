using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ProjectRepositry : IProjectRepositry
    {
        private readonly ApplictionDbContext _context;


        public ProjectRepositry(ApplictionDbContext context)
        {
            _context = context;
        }


        public void Addprojetc(Project project)
        {
            try
            {
                // Add the user to the context and save changes to the database
                _context.Project.Add(project);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception (could be a database error, validation error, etc.)
                Console.WriteLine($"An error occurred while adding the project: {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred while adding the prooject.", ex);
            }
        }

        //get project by id project 
        public Project GetProjectByID(int pid)
        {
            try
            {
                return _context.Project.FirstOrDefault(c => c.ProjectID == pid);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred : {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred .", ex);
            }
        }

        //get project by id project 
        public List<Project> GetProjectsByDevID(int DevID)
        {
            try
            {
                return _context.Project.Where(p => p.DeveloperID == DevID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred : {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred .", ex);
            }
        }

        //get project by id project 
        public Project GetProjectByIDClient(int clientid)
        {
            try
            {
                return _context.Project.FirstOrDefault(c => c.ClientID == clientid);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred", ex);
            }
        }
        //soft delete project 
        public bool DeleteProject(Project pro)
        {
            try
            {
                _context.Project.Remove(pro);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred", ex);
            }
        }
        //soft update by project 
        public void UpdateProject(Project pro)
        {
            try
            {
                _context.Project.Update(pro);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                // Optionally, you could throw the exception to be handled by a higher level
                throw new Exception("An error occurred", ex);
            }
        }
    }
}
