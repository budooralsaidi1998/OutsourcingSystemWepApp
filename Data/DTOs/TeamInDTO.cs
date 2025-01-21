using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class TeamInDTO
    {
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
    }
}
