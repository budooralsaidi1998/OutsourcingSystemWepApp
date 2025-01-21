using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class Skill
    {

        [Key]
        public int SkillID { get; set; }

        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(100, ErrorMessage = "Skill name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Skill description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Active status is required.")]
        public bool Active { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<DeveloperSkill> DeveloperSkills { get; set; }
    }
}
