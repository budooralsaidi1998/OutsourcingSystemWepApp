using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class RegisterClientDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Industry is required")]
        public string Industry { get; set; } = string.Empty;
    }
}
