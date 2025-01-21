using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IDeveloperServices
    {
        IEnumerable<Developer> GetAll();
        IEnumerable<filtrationDeveloperdto> GetAlldeveloper(string name, string speclization, decimal? rating, bool? availiabilty, int pageNumber = 1, int pageSize = 10);
        IEnumerable<filtrationDeveloperdto> GetByAvailability(bool av);
        Developer GetById(int id);
        IEnumerable<filtrationDeveloperdto> GetName(string name);
        IEnumerable<filtrationDeveloperdto> Getrate(decimal rating);
        IEnumerable<filtrationDeveloperdto> GetSpecilization(string spec);
        void RegisterDeveloper(UserDeveloperInputDto input);
        void SoftDeleteClient(int id);
        void UpdateDeveloper(int id, UpdateDeveInput updateDeveloper);
    }
}