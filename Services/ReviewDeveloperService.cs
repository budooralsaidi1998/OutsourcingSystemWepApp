using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class ReviewDeveloperService : IReviewDeveloperService
    {
        private readonly IReviewDevRepository _reviewDevRepository;
        public ReviewDeveloperService(IReviewDevRepository reviewDevRepository)
        {
            _reviewDevRepository = reviewDevRepository;
        }

        //Adds developer review using input from user --this is called from the joint service to avoid cycle dependacy error
        //This is becuase it uses multiple different services 
        public int AddReviewDev(int ClientID, ClientReviewInDTO input)
        {
            //check that client did not already add review for this client
            var alreadyReviewed = _reviewDevRepository.GetReviewByClientAndTeamIDs(ClientID, ClientID);

            if (alreadyReviewed == null)
            {
                //mapping ReviewDevInDTO to ReviewDev
                var Dreview = new ClientReviewDeveloper
                {
                    ClientID = ClientID,
                    DeveloperID = input.ID,
                    Rating = input.Rating,
                    Comment = input.Comment,
                };

                return _reviewDevRepository.AddDevReview(Dreview);
            }
            else return -2; //already reviewed this team you can update your existing review
        }

        //Checks if dev review exists
        public bool CheckReviewByDevID(int DevID)
        {
            var review = _reviewDevRepository.GetDevReviewByDevID(DevID);
            return review != null ? true : false; //if team found return true if not found return false
        }

        public bool CheckReviewByDevIDandTeamID(int ClientID, int DevID)
        {
            var review = _reviewDevRepository.GetReviewByClientAndTeamIDs(ClientID, DevID);
            return review != null ? true : false; //if team found return true if not found return false
        }

        public ClientReviewDeveloper GetByDevIDandTeamID(int ClientID, int DevID)
        {
            return _reviewDevRepository.GetReviewByClientAndTeamIDs(ClientID, DevID);
        }

        public void DeleteDeveloperReview(ClientReviewDeveloper devrev)
        {
            _reviewDevRepository.DeleteDevReview(devrev);
        }

        public ClientReviewDeveloper GetReviewByDevID(int DevID)
        {
            return _reviewDevRepository.GetDevReviewByDevID(DevID);
        }

        //Updates review using input from user 
        public int UpdateDevReview(int ClientID, ClientReviewInDTO review)
        {
            var oldReview = _reviewDevRepository.GetDevReviewByDevID(review.ID);

            if (oldReview != null)
            {
                //mapping ClientReviewInDTO to ClientReviewDev 
                oldReview.Comment = review.Comment;
                oldReview.Rating = review.Rating;
                oldReview.Date = DateTime.Now;

                _reviewDevRepository.UpdateDevReview(oldReview);
                return 0; //no errors 
            }
            else return 1; //team not found 
        }

        public ClientReviewDeveloper GetReviewByDevIdnames(int DevID)
        {
            var review = _reviewDevRepository.GetDevReviewByDevID(DevID);
            if (review != null)
            {
                return new ClientReviewDeveloper
                {
                    ReviewID = review.ReviewID,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    Date = review.Date,
                    Developer = review.Developer,
                    Client = review.Client
                };
            }
            return null;
        }


        public List<ClientReviewDeveloper> GetAllDevReviews(int Page, int PageSize, int? Rating, int? DevID)
        {
            var reviewTeams = _reviewDevRepository.GetAllDevReviews();

            // Filters by rating
            if (Rating.HasValue)
            {
                reviewTeams = reviewTeams.Where(t => t.Rating >= Rating).ToList();
            }

            // Filters by teamID
            if (DevID.HasValue)
            {
                reviewTeams = reviewTeams.Where(t => t.DeveloperID == DevID).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return reviewTeams.OrderByDescending(t => t.Rating).Skip(number).Take(PageSize).ToList();
        }

        public List<ClientReviewDeveloper> GetAllDevReviews(int Page, int PageSize)
        {
            var reviewTeams = _reviewDevRepository.GetAllDevReviewsnames();
            int number = PageSize * Page;
            return reviewTeams.OrderByDescending(t => t.Rating).Skip(number).Take(PageSize).ToList();
        }

       
    }
    }
