using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ClientRequestDeveloperRepository : IClientRequestDeveloperRepository
    {
        private readonly ApplictionDbContext _context;

        public ClientRequestDeveloperRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task AddRequestAsync(ClientRequestDeveloper request)
        {
            await _context.ClientRequestDeveloper.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientRequestDeveloper> GetRequestByIdAsync(int requestId)
        {
            return await _context.ClientRequestDeveloper.FindAsync(requestId);
        }

        public async Task UpdateRequestAsync(ClientRequestDeveloper request)
        {
            _context.ClientRequestDeveloper.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientRequestDeveloper>> GetPendingRequestsAsync()
        {
            return await Task.Run(() => _context.ClientRequestDeveloper.Where(req => req.Status == "Pending").Include(S=>S.Client).ToList());
        }

        public async Task<IEnumerable<ClientRequestDeveloper>> ApprovedRequest()
        {
            try
            {
                // Direct asynchronous query without Task.Run()
                return await _context.ClientRequestDeveloper
                    .Where(req => req.Status == "Approved") // Check if Status is exactly "Approved"
                    .Include(req => req.Client)  // Ensure that 'Client' is a valid navigation property
                    .ToListAsync();  // Use ToListAsync() for EF Core asynchronous queries
            }
            catch (Exception e)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error fetching approved requests: {e.Message}");
                // Optionally rethrow or return an empty list or null
                return Enumerable.Empty<ClientRequestDeveloper>();
            }
        }


        public async Task<IEnumerable<ClientRequestDeveloper>> RejectedRequest()
        {
            return await Task.Run(() => _context.ClientRequestDeveloper.Where(req => req.Status == "Rejected").Include(S => S.Client).ToList());
        }
        public List<ClientRequestDeveloper> GetDeveloperReview(int clientid , int  developerid)
        {
           return _context.ClientRequestDeveloper.Where(d => d.Status=="Approved" && d.ClientID==clientid&& d.DeveloperID==developerid).ToList();
        }
    }
}
