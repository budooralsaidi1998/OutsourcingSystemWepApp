using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IFeedBackOnClientService
    {
        int AddFeedbackOnClient(int ReviewerID, FeedBackOnClientDTO feedback, int OnTeam);
        string DeleteFeedback(int ClientID, int ReviewerID);
        List<FeedbackOnClient> GetAllFeedbacks(int Page, int PageSize, int? Rating, int? ClientID);
        List<FeedbackOnClient> GetFeedbackByClientID(int ClientID);
        FeedbackOnClient GetRevTeamByDevIDAndClientID(int DevID, int ClientID);
        FeedbackOnClient GetRevTeamByTeamIDAndClientID(int TeamID, int ClientID);
        int UpdateFeedback(int DevID, FeedBackOnClientDTO feedback);
    }
}