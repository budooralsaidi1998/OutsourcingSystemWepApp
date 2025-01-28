using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<PendingRequestDto>> GetPendingRequestsAsync();
        Task ProcessRequestAsync(int requestId, bool isAccepted, string requestType);
        Task SubmitRequestAsync(int userid, ProjectRequestInputDto project);
        List<ClientRequestDeveloper> GetDeveloperApprove(int cid, int devid);
       // Task<IEnumerable<PendingRequestDto>> GetAcceptAndReject();
    }
}