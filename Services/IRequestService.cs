using OutsourcingSystemWepApp.Data.DTOs;

namespace OutsourcingSystemWepApp.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<PendingRequestDto>> GetPendingRequestsAsync();
        Task ProcessRequestAsync(int requestId, bool isAccepted, string requestType);
        Task SubmitRequestAsync(int userid, ProjectRequestInputDto project);
       // Task<IEnumerable<PendingRequestDto>> GetAcceptAndReject();
    }
}