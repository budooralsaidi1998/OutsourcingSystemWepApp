using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IProjectServieces
    {
        void AddProject(int idclienttoken, ProjectRequestInputDto project);
        Project GetProjectByClientId(int id);
        Project GetProjectById(int id);
        bool SoftDeleteClient(int id);
        void UpdateProject(int id, UpdateProjectDto project);
        List<Project> GetProjectsByDevID(int DevID);
    }
}