namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class DeveloperDTOForProfile
    {
        public int DeveloperID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public int Age { get; set; }
        public decimal HourlyRate { get; set; }
        public string CareerSummary { get; set; }
        public int CompletedProjects { get; set; }
        public string CurrentProject { get; set; }
        public string DocumentLink { get; set; }

        public List<DeveloperSkillDTO> Skills { get; set; } = new List<DeveloperSkillDTO>();
    }
}
