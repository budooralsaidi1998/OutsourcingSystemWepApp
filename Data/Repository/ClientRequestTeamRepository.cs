using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ClientRequestTeamRepository : IClientRequestTeamRepository
    {
        private readonly ApplictionDbContext _context;

        public ClientRequestTeamRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task AddRequestAsync(ClientRequestTeam request)
        {
            await _context.ClientRequestTeam.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientRequestTeam> GetRequestByIdAsync(int requestId)
        {
            return await _context.ClientRequestTeam.FindAsync(requestId);
        }

        public async Task UpdateRequestAsync(ClientRequestTeam request)
        {
            _context.ClientRequestTeam.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientRequestTeam>> GetPendingRequestsAsync()
        {
            return await Task.Run(() => _context.ClientRequestTeam.Where(req => req.Status == "Pending").ToList());
        }
    }
}
