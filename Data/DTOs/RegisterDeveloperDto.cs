using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class RegisterDeveloperDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Years of Experience is required.")]
        [Range(1, 50, ErrorMessage = "Years of Experience must be between 1 and 50.")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(1, 1000, ErrorMessage = "Hourly Rate must be between 1 and 1000.")]
        public decimal HourlyRate { get; set; }

        [Required(ErrorMessage = "Career Summary is required.")]
        public string CareerSummary { get; set; } = string.Empty;


        public string DocumentLink { get; set; } = string.Empty;
    }
}
