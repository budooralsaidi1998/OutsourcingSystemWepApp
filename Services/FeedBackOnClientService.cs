using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class FeedBackOnClientService : IFeedBackOnClientService
    {
        private readonly IFeedBackOnClientRepository _feedBackOnClientRepository;
        public FeedBackOnClientService(IFeedBackOnClientRepository feedbackrepo)
        {
            _feedBackOnClientRepository = feedbackrepo;
        }

        public int AddFeedbackOnClient(int ReviewerID, FeedBackOnClientDTO feedback, int OnTeam)
        {
            //check if team or developer and enter their id in their correct area
            //map DTO 


            //check that client did not already add review for this client
            var alreadyReviewed = _feedBackOnClientRepository.GetReviewByClientAndDeveloperIDs(feedback.ClientID, ReviewerID);

            if (alreadyReviewed == null)
            {
                if (OnTeam == null)
                {
                    //mapping feedbackDTO to feedback 
                    var Feedback = new FeedbackOnClient
                    {
                        ClientID = feedback.ClientID,
                        DeveloperID = ReviewerID,
                        Rating = feedback.Rating,
                        Comment = feedback.Comment,
                        Date = DateTime.Now,
                    };
                    return _feedBackOnClientRepository.AddFeedback(Feedback);

                }

                else
                {
                    //mapping feedbackDTO to feedback 
                    var Feedback = new FeedbackOnClient
                    {
                        ClientID = feedback.ClientID,
                        DeveloperID = ReviewerID,
                        TeamID = OnTeam,
                        Rating = feedback.Rating,
                        Comment = feedback.Comment,
                        Date = DateTime.Now,
                    };
                    return _feedBackOnClientRepository.AddFeedback(Feedback);
                }
            }
            else return -2; //already reviewed this team you can update your existing review
        }

        //Gets the feedback [team] -> feedback
        public FeedbackOnClient GetRevTeamByTeamIDAndClientID(int TeamID, int ClientID)
        {
            return _feedBackOnClientRepository.GetReviewByClientAndTeamIDs(ClientID, TeamID);
        }

        //Gets the feedback [developer] -> feedback
        public FeedbackOnClient GetRevTeamByDevIDAndClientID(int DevID, int ClientID)
        {
            return _feedBackOnClientRepository.GetReviewByClientAndDeveloperIDs(ClientID, DevID);
        }

        public List<FeedbackOnClient> GetFeedbackByClientID(int ClientID)
        {
            return _feedBackOnClientRepository.GetAllFeedbacksByClient(ClientID).ToList();
        }

        //Updates feedback using input from user 
        public int UpdateFeedback(int DevID, FeedBackOnClientDTO feedback)
        {
            var oldfeedback = _feedBackOnClientRepository.GetReviewByClientAndDeveloperIDs(feedback.ClientID, DevID);

            if (oldfeedback != null)
            {
                //mapping FeedbackInDTO to FeedbackDTO 
                oldfeedback.Comment = feedback.Comment;
                oldfeedback.Rating = feedback.Rating;
                oldfeedback.Date = DateTime.Now;

                _feedBackOnClientRepository.UpdateFeedback(oldfeedback);
                return 0; //no errors 
            }

            else return 1; //team not found 
        }

        public string DeleteFeedback(int ClientID, int ReviewerID)
        {
            var feedback = _feedBackOnClientRepository.GetReviewByClientAndDeveloperIDs(ClientID, ReviewerID);

            if (feedback != null)
            {
                _feedBackOnClientRepository.DeleteFeedback(feedback);
                return "Deleted!";
            }
            else return "<!>This feedback does not exist<!>";
        }


        public List<FeedbackOnClient> GetAllFeedbacks(int Page, int PageSize, int? Rating, int? ClientID)
        {
            var clientrevs = _feedBackOnClientRepository.GetAllFeedbacks();

            // Filters by rating
            if (Rating.HasValue)
            {
                clientrevs = clientrevs.Where(t => t.Rating >= Rating).ToList();
            }

            // Filters by teamID
            if (ClientID.HasValue)
            {
                clientrevs = clientrevs.Where(t => t.ClientID == ClientID).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return clientrevs.OrderByDescending(t => t.Rating).Skip(number).Take(PageSize).ToList();
        }
    }
}
