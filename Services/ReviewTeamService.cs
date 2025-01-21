using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class ReviewTeamService : IReviewTeamService
    {
        private readonly IReviewTeamRepository _reviewTeamRepository;
        public ReviewTeamService(IReviewTeamRepository revTeamrepo)
        {
            _reviewTeamRepository = revTeamrepo;
        }

        //Adds team review using input from user --this is called from the joint service to avoid cycle dependacy error
        //This is becuase it uses multiple different services 
        public int AddReviewTeam(int ClientID, ClientReviewInDTO input)
        {
            //check that client did not already add review for this client
            var alreadyReviewed = _reviewTeamRepository.GetReviewByClientAndTeamIDs(ClientID, input.ID);

            if (alreadyReviewed == null)
            {
                //mapping ReviewTeamInDTO to Reviewteam 
                var Treview = new ClientReviewTeam
                {
                    ClientID = ClientID,
                    TeamID = input.ID,
                    Rating = input.Rating,
                    Comment = input.Comment,
                };

                return _reviewTeamRepository.AddTeamReview(Treview);
            }
            else return -2; //already reviewed this team you can update your existing review
        }

        //Checks if the review exists
        public bool CheckReviewByTeamID(int TeamID)
        {
            var review = _reviewTeamRepository.GetTeamReviewByTeamID(TeamID);
            return review != null ? true : false; //if team found return true if not found return false
        }

        //Checks if the review exists
        public bool CheckReviewByTeamIDAndClientID(int TeamID, int ClientID)
        {
            var review = _reviewTeamRepository.GetReviewByClientAndTeamIDs(ClientID, TeamID);
            return review != null ? true : false; //if team found return true if not found return false
        }

        public ClientReviewTeam GetRevTeamByTeamIDAndClientID(int TeamID, int ClientID)
        {
            return _reviewTeamRepository.GetReviewByClientAndTeamIDs(ClientID, TeamID);
        }

        public ClientReviewTeam GetReviewByTeamID(int TeamID)
        {
            return _reviewTeamRepository.GetTeamReviewByTeamID(TeamID);
        }

        //Updates review using input from user 
        public int UpdateTeamReview(int ClientID, ClientReviewInDTO review)
        {
            var oldReview = _reviewTeamRepository.GetTeamReviewByTeamID(review.ID);

            if (oldReview != null)
            {
                //mapping ClientReviewTeamInDTO to ClientReviewTeam 
                oldReview.Comment = review.Comment;
                oldReview.Rating = review.Rating;
                oldReview.Date = DateTime.Now;

                _reviewTeamRepository.UpdateTeamReview(oldReview);
                return 0; //no errors 
            }

            else return 1; //team not found 
        }

        public void DeleteTeamReview(ClientReviewTeam teamRev)
        {
            _reviewTeamRepository.DeleteTeamReview(teamRev);
        }


        public List<ClientReviewTeam> GetAllTeamReviews(int Page, int PageSize, int? Rating, int? TeamID)
        {
            var reviewTeams = _reviewTeamRepository.GetAllTeamReviews();

            // Filters by rating
            if (Rating.HasValue)
            {
                reviewTeams = reviewTeams.Where(t => t.Rating >= Rating).ToList();
            }

            // Filters by teamID
            if (TeamID.HasValue)
            {
                reviewTeams = reviewTeams.Where(t => t.TeamID == TeamID).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return reviewTeams.OrderByDescending(t => t.Rating).Skip(number).Take(PageSize).ToList();
        }
    }
}
