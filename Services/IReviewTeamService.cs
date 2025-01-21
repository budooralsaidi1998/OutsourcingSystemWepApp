using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IReviewTeamService
    {
        int AddReviewTeam(int ClientID, ClientReviewInDTO input);
        bool CheckReviewByTeamID(int TeamID);
        bool CheckReviewByTeamIDAndClientID(int TeamID, int ClientID);
        void DeleteTeamReview(ClientReviewTeam teamRev);
        List<ClientReviewTeam> GetAllTeamReviews(int Page, int PageSize, int? Rating, int? TeamID);
        ClientReviewTeam GetReviewByTeamID(int TeamID);
        ClientReviewTeam GetRevTeamByTeamIDAndClientID(int TeamID, int ClientID);
        int UpdateTeamReview(int ClientID, ClientReviewInDTO review);
    }
}