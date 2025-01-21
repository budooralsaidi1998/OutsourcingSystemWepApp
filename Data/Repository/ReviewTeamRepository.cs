using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ReviewTeamRepository : IReviewTeamRepository
    {
        private readonly ApplictionDbContext _context;

        public ReviewTeamRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new TeamReview [returns review id]
        public int AddTeamReview(ClientReviewTeam teamRev)
        {
            _context.ClientReviewTeam.Add(teamRev);
            _context.SaveChanges();
            return teamRev.ReviewID;
        }

        //Update TeamReview [does not return anything]
        public void UpdateTeamReview(ClientReviewTeam TeamRev)
        {
            _context.ClientReviewTeam.Update(TeamRev);
            _context.SaveChanges();
        }

        //Hard deleting Team review
        public void DeleteTeamReview(ClientReviewTeam TeamRev)
        {
            _context.ClientReviewTeam.Remove(TeamRev);
        }

        //Gets all TeamReviews [returns list of team reviews]
        public List<ClientReviewTeam> GetAllTeamReviews()
        {
            return _context.ClientReviewTeam.ToList();
        }

        //Get TeamReview by TeamID [returns teamReview by ID]
        public ClientReviewTeam GetTeamReviewByTeamID(int TeamID)
        {
            return _context.ClientReviewTeam.FirstOrDefault(r => r.TeamID == TeamID);
        }

        public ClientReviewTeam GetReviewByClientAndTeamIDs(int ClientID, int TeamID)
        {
            return _context.ClientReviewTeam.FirstOrDefault(r => r.TeamID == TeamID && r.ClientID == ClientID);
        }
    }
}
