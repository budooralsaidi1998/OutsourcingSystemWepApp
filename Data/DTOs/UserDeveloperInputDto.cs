using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class UserDeveloperInputDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Nmae cannot more than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }




        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Developer|Admin|Client)$", ErrorMessage = "The role must be one of the following: Developer, Admin, Client.")]
        public string role { get; set; }   //"client " , "Developer" , "Admin"


        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Years of experience is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Years of experience must be a non-negative number.")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Hourly rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than zero.")]
        public decimal HourlyRate { get; set; }

        //[Required(ErrorMessage = "Availability status is required.")]
        //public bool AvailabilityStatus { get; set; } = true;

        [MaxLength(1000, ErrorMessage = "Career summary cannot exceed 1000 characters.")]
        public string CareerSummary { get; set; }

        //  [Range(0, int.MaxValue, ErrorMessage = "Completed projects must be a non-negative number.")]
        //  public int CompletedProjects { get; set; } = 0;

        [Required(ErrorMessage = "CanBePartOfTeam is required.")]
        // public bool CanBePartOfTeam { get; set; } = true;

        [Url(ErrorMessage = "Invalid URL format.")]
        public string DocumentLink { get; set; }

        public string DeveloperName { get; set; }
    }
}
