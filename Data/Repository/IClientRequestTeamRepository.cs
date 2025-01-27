using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IClientRequestTeamRepository
    {
        Task AddRequestAsync(ClientRequestTeam request);
        Task<IEnumerable<ClientRequestTeam>> GetPendingRequestsAsync();
        Task<ClientRequestTeam> GetRequestByIdAsync(int requestId);
        Task UpdateRequestAsync(ClientRequestTeam request);
        Task<IEnumerable<ClientRequestTeam>> ApprovedRequest();
        Task<IEnumerable<ClientRequestTeam>> RejectedRequest();
    }
}