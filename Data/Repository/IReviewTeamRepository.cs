using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IReviewTeamRepository
    {
        int AddTeamReview(ClientReviewTeam teamRev);
        void DeleteTeamReview(ClientReviewTeam TeamRev);
        List<ClientReviewTeam> GetAllTeamReviews();
        ClientReviewTeam GetReviewByClientAndTeamIDs(int ClientID, int TeamID);
        ClientReviewTeam GetTeamReviewByTeamID(int TeamID);
        void UpdateTeamReview(ClientReviewTeam TeamRev);
    }
}