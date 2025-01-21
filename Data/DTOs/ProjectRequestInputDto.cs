using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class ProjectRequestInputDto
    {
        //public int ClientIDinrequest { get; set; }
        //[Required]
        //public DateTime StartDate { get; set; }

        //[Required]
        //public DateTime EndDate { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Status { get; set; } = "Pending";


        //  public int ProjectID { get; set; }

        // public int ClientId { get; set; }


        public int? Teamid { get; set; }


        [Required(ErrorMessage = "Project name is required.")]
        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        //[Required(ErrorMessage = "Status is required.")]
        //public string Statusinproject { get; set; } = "Ongoing"; // Pending, Ongoing, Completed


        public int? Developerid { get; set; }


        [Required(ErrorMessage = "Daily hours needed is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Daily hours needed must be at least 1.")]
        public int DailyHoursNeeded { get; set; }
        public string RequestType { get; set; }
    }
}
