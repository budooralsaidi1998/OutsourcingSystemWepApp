using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class FeedBackOnClientRepository : IFeedBackOnClientRepository
    {
        private readonly ApplictionDbContext _context;

        public FeedBackOnClientRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new Feedback [returns feedback id]
        public int AddFeedback(FeedbackOnClient feedback)
        {
            _context.FeedbackOnClient.Add(feedback);
            _context.SaveChanges();
            return feedback.FeedbackID;
        }

        //Update Feedback [does not return anything]
        public void UpdateFeedback(FeedbackOnClient feedback)
        {
            _context.FeedbackOnClient.Update(feedback);
            _context.SaveChanges();
        }

        //Hard deleting feedback
        public void DeleteFeedback(FeedbackOnClient feedback)
        {
            _context.FeedbackOnClient.Remove(feedback);
        }

        //Gets all Feedbacks [returns list of all feedbacks]
        public List<FeedbackOnClient> GetAllFeedbacks()
        {
            return _context.FeedbackOnClient.ToList();
        }

        //Gets all Feedbacks by clientID [returns list of feedbacks]
        public List<FeedbackOnClient> GetAllFeedbacksByClient(int ClientID)
        {
            return _context.FeedbackOnClient.ToList();
        }

        public FeedbackOnClient GetReviewByFeedbackID(int feedbackID)
        {
            return _context.FeedbackOnClient.FirstOrDefault(f => f.FeedbackID == feedbackID);
        }

        //Gets review by the teamID and clientID 
        public FeedbackOnClient GetReviewByClientAndTeamIDs(int ClientID, int TeamID)
        {
            return _context.FeedbackOnClient.FirstOrDefault(r => r.TeamID == TeamID && r.ClientID == ClientID);
        }

        //Gets review by DeveloperID and clientID
        public FeedbackOnClient GetReviewByClientAndDeveloperIDs(int ClientID, int DeveloperID)
        {
            return _context.FeedbackOnClient.FirstOrDefault(r => r.DeveloperID == DeveloperID && r.ClientID == ClientID);
        }

        public FeedbackOnClient GetByFeedbackID(int FeedbackID)
        {
            return _context.FeedbackOnClient.FirstOrDefault(r => r.FeedbackID == FeedbackID);
        }
    }
}
