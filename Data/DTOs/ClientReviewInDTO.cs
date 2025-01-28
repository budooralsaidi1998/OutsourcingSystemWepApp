using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class ClientReviewInDTO
    {
        //public int ClientID { get; set; }

        public int ID { get; set; } //team or dev ID


        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string Comment { get; set; }
        public int ReviewID { get; set; }
       // This could be either Team or Developer ID
        public string EntityName { get; set; } // Name of Team or Developer
        public string ClientName { get; set; }
     
        public DateTime Date { get; set; }
        public bool IsDeveloperReview { get; set; }
        public List<int> SelectedDevelopers { get; set; } = new List<int>();
    }
}
