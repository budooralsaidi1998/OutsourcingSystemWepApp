using OutsourcingSystemWepApp.Data.DTOs;

namespace OutsourcingSystemWepApp.Services
{
    public interface IClientService
    {
        IEnumerable<ClientDTO> GetAllClients(string name, string industry, decimal? rating, int pageNumber, int pageSize);
        ClientDTO GetClientById(int id);
        updateClientDtocs GetClientProfile(int clientId);
        IEnumerable<ClientByIndestry> GetClientsByIndustry(string industry);
        IEnumerable<ClientByIndestry> GetClientsByRating(decimal rating);
        List<string> GetCurrentProjects(int clientId);
        List<string> GetPreviousProjects(int clientId);
        Task<bool> RegisterClient(RegisterClientDto clientDto);
        void SoftDeleteClient(int id);
        void UpdateClient(int id, UpdateClientData updatedClientDto);
        void UpdateIndustry(int clientId, string industry);
    }
}