using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ReviewDevRepository : IReviewDevRepository
    {
        private readonly ApplictionDbContext _context;

        public ReviewDevRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new DevReview [returns review id]
        public int AddDevReview(ClientReviewDeveloper DevRev)
        {
            _context.ClientReviewDeveloper.Add(DevRev);
            _context.SaveChanges();
            return DevRev.ReviewID;
        }

        //Update DevReview [does not return anything]
        public void UpdateDevReview(ClientReviewDeveloper DevRev)
        {
            _context.ClientReviewDeveloper.Update(DevRev);
            _context.SaveChanges();
        }

        //Hard deleting Developer review
        public void DeleteDevReview(ClientReviewDeveloper DevRev)
        {
            _context.ClientReviewDeveloper.Remove(DevRev);
        }

        //Gets all DeveloperReviews [returns list of developer reviews]
        public List<ClientReviewDeveloper> GetAllDevReviews()
        {
            return _context.ClientReviewDeveloper.ToList();
        }

        //Get DeveloperReview by DeveloperID [returns devReview by ID]
        public ClientReviewDeveloper GetDevReviewByDevID(int DevID)
        {
            return _context.ClientReviewDeveloper.FirstOrDefault(r => r.DeveloperID == DevID);
        }

        public ClientReviewDeveloper GetReviewByClientAndTeamIDs(int ClientID, int DevID)
        {
            return _context.ClientReviewDeveloper.FirstOrDefault(r => r.DeveloperID == DevID && r.ClientID == ClientID);
        }
       
        public List<ClientReviewDeveloper> GetAllDevReviewsnames()
        {
            return _context.ClientReviewDeveloper
                .Include(r => r.Developer)   // Include developer details
                .Include(r => r.Client)      // Include client details
                .ToList();
        }
    }
}
