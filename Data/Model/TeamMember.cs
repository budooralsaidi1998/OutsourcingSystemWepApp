using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutsourcingSystemWepApp.Data.Model
{
    [PrimaryKey(nameof(TeamID), nameof(DeveloperID))]
    public class TeamMember
    {
        [ForeignKey(nameof(Team))]
        public int TeamID { get; set; }
        public Team Team { get; set; }



        [ForeignKey(nameof(Developer))]
        public int DeveloperID { get; set; }
        public Developer Developer { get; set; }

       // [Required(ErrorMessage = "Join date is required.")]
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
