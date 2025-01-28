using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IReviewDeveloperService
    {
        int AddReviewDev(int ClientID, ClientReviewInDTO input);
        bool CheckReviewByDevID(int DevID);
        bool CheckReviewByDevIDandClientID(int ClientID, int DevID);
        void DeleteDeveloperReview(ClientReviewDeveloper devrev);
        List<ClientReviewDeveloper> GetAllDevReviews(int Page, int PageSize);
        List<ClientReviewDeveloper> GetAllDevReviews(int Page, int PageSize, int? Rating, int? DevID);
        ClientReviewDeveloper GetByDevIDandTeamID(int ClientID, int DevID);
        ClientReviewDeveloper GetReviewByDevID(int DevID);
        ClientReviewDeveloper GetReviewByDevIdnames(int DevID);
        int UpdateDevReview(int ClientID, ClientReviewInDTO review);
    }
}