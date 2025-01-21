using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UID { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(100, ErrorMessage = "Company name cannot more than 100 characters.")]
        public string CompanyName { get; set; }

        [MaxLength(100, ErrorMessage = "Industry name cannot than more 100 characters.")]
        public string Industry { get; set; }

        [Range(0, 5, ErrorMessage = "Commitment rating must be between 0 and 5.")]
        public decimal CommitmentRating { get; set; }

        [MaxLength(1000, ErrorMessage = "Notes cannot more than 1000 characters.")]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<ClientRequestDeveloper> ClientRequestDeveloper { get; set; }

        public virtual ICollection<ClientRequestTeam> ClientRequestTeam { get; set; }

        public virtual ICollection<ClientReviewDeveloper> ClientReviewDeveloper { get; set; }

        public virtual ICollection<ClientReviewTeam> ClientReviewTeam { get; set; }

        public virtual ICollection<FeedbackOnClient> FeedbackOnClient { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
