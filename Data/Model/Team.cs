using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class Team
    {

        [Key]
        public int TeamID { get; set; }



        [Required(ErrorMessage = "Team name is required.")]
        [MaxLength(100, ErrorMessage = "Team name cannot exceed 100 characters.")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Team capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Team capacity must be at least 1.")]
        public int TeamCapacity { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a non-negative value.")]
        public decimal HourlyRate { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal Rating { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Completed projects must be a non-negative number.")]
        public int CompletedProjects { get; set; } = 0;
        public bool Active { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }

        public virtual ICollection<ClientReviewTeam> ClientReviewTeam { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<FeedbackOnClient> FeedbackOnClient { get; set; }
    }
}
