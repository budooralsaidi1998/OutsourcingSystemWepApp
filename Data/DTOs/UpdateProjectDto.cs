using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class UpdateProjectDto
    {
        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
