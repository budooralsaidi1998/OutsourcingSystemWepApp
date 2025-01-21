using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class FeedBackOnClientDTO
    {
        [Required(ErrorMessage = "ID is required.")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string Comment { get; set; }
    }
}
