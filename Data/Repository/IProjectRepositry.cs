using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IProjectRepositry
    {
        void Addprojetc(Project project);
        bool DeleteProject(Project pro);
        Project GetProjectByID(int pid);
        Project GetProjectByIDClient(int clientid);
        void UpdateProject(Project pro);
        List<Project> GetProjectsByDevID(int DevID);
    }
}