using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        IEnumerable<Client> GetByIndustry(string industry);
        IEnumerable<Client> GetByRating(decimal rating);
        Client GetByuid(int id);
        Client GetClientProfile(int clientId);
        List<string> GetCurrentProjects(int clientId);
        List<string> GetPreviousProjects(int clientId);
        void SoftDelete(Client client);
        void Update(Client client);
        void UpdateIndustry(int clientId, string industry);
    }
}