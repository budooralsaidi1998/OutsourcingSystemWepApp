using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IFeedBackOnClientRepository
    {
        int AddFeedback(FeedbackOnClient feedback);
        void DeleteFeedback(FeedbackOnClient feedback);
        List<FeedbackOnClient> GetAllFeedbacks();
        List<FeedbackOnClient> GetAllFeedbacksByClient(int ClientID);
        FeedbackOnClient GetByFeedbackID(int FeedbackID);
        FeedbackOnClient GetReviewByClientAndDeveloperIDs(int ClientID, int DeveloperID);
        FeedbackOnClient GetReviewByClientAndTeamIDs(int ClientID, int TeamID);
        FeedbackOnClient GetReviewByFeedbackID(int feedbackID);
        void UpdateFeedback(FeedbackOnClient feedback);
    }
}