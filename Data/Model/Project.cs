using static MudBlazor.Colors;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class Project
    {

        [Key]
        public int ProjectID { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Required]

        [ForeignKey(nameof(Teams))]
        public int? TeamID { get; set; }
        public Team Teams { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = "Ongoing"; // Pending, Ongoing, Completed

        [ForeignKey(nameof(Developer))]
        public int? DeveloperID { get; set; }
        public Developer Developer { get; set; }

        [Required(ErrorMessage = "Daily hours needed is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Daily hours needed must be at least 1.")]
        public int DailyHoursNeeded { get; set; }

        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
