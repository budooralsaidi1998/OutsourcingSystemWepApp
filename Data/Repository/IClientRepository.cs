using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IClientRepository
    {
        void Add(Client client);
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        IEnumerable<Client> GetByIndustry(string industry);
        IEnumerable<Client> GetByRating(decimal rating);
        Client GetByuid(int id);
        void SoftDelete(Client client);
        void Update(Client client);
    }
}