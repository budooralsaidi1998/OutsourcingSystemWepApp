using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IReviewDevRepository
    {
        int AddDevReview(ClientReviewDeveloper DevRev);
        void DeleteDevReview(ClientReviewDeveloper DevRev);
        List<ClientReviewDeveloper> GetAllDevReviews();
        ClientReviewDeveloper GetDevReviewByDevID(int DevID);
        ClientReviewDeveloper GetReviewByClientAndTeamIDs(int ClientID, int DevID);
        void UpdateDevReview(ClientReviewDeveloper DevRev);
        List<ClientReviewDeveloper> GetAllDevReviewsnames();
    }
}