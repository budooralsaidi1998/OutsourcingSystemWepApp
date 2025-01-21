using static MudBlazor.Colors;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class FeedbackOnClient
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(Developer))]
        public int? DeveloperID { get; set; }
        public Developer Developer { get; set; }

        [ForeignKey(nameof(Team))]
        public int? TeamID { get; set; }
        public Team Team { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
