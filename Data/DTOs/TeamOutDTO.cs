namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class TeamOutDTO
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int TeamCapacity { get; set; }
        public string Description { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Rating { get; set; }
        public int CompletedProjects { get; set; }
        public bool Active { get; set; } = true;
        public bool IsAvailable { get; set; } 
    }
}
