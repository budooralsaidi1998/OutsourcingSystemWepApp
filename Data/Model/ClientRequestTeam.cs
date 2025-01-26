using static MudBlazor.Colors;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class ClientRequestTeam
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(team))]
        public int TID { get; set; }
        public Team team { get; set; }


        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = "Pending";// Pending, Approved, Rejected
    }
}
