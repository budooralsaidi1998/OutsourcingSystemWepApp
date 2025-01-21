using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IJointService
    {
        int AddReviewDeveloper(int DevID, ClientReviewInDTO input);
        int AddReviewTeam(int ClientID, ClientReviewInDTO input);
        string AddTeamMemberToTeam(int developerID, int teamID);
        string DeleteDeveloperReview(int ClientID, int DevID);
        string DeleteFeedbackOnClient(int DevID, int clientID);
        string DeleteTeamReview(int ClientID, int TeamID);
        int FeedbackValidation(int DevID, FeedBackOnClientDTO feedback);
        List<FeedbackOnClient> GetClientFeedback(int Page, int PageSize, int? Rating, int? ClientID);
        List<ClientReviewDeveloper> GetDeveloperReviews(int Page, int PageSize, int? Rating, int? DevID);
        List<TeamMember> GetTeamMemberByTeamID(int teamID);
        List<ClientReviewTeam> GetTeamReviews(int Page, int PageSize, int? Rating, int? TeamID);
        string RemoveTeamMemberFromTeam(int developerID, int teamID);
        int UpdateFeebackOnClient(int DevID, FeedBackOnClientDTO feedback);
        int UpdateReviewDeveloper(int ClientID, ClientReviewInDTO review);
        int UpdateReviewTeam(int ClientID, ClientReviewInDTO review);
    }
}