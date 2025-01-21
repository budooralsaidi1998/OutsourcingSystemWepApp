using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class filtrationDeveloperdto
    {
        public string DeveloperName { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; }

        [Range(0, 5, ErrorMessage = "Commitment rating must be between 0 and 5.")]
        public decimal CommitmentRating { get; set; }

        [Required(ErrorMessage = "Availability status is required.")]
        public bool AvailabilityStatus { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Completed projects must be a non-negative number.")]
        public int CompletedProjects { get; set; } = 0;

    }
}
