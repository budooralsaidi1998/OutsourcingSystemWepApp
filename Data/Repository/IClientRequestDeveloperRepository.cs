using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IClientRequestDeveloperRepository
    {
        Task AddRequestAsync(ClientRequestDeveloper request);
        Task<IEnumerable<ClientRequestDeveloper>> GetPendingRequestsAsync();
        Task<ClientRequestDeveloper> GetRequestByIdAsync(int requestId);
        Task UpdateRequestAsync(ClientRequestDeveloper request);
        Task<IEnumerable<ClientRequestDeveloper>> ApprovedRequest();
        Task<IEnumerable<ClientRequestDeveloper>> RejectedRequest();
        List<ClientRequestDeveloper> GetDeveloperReview(int clientid, int developerid);
    }
}