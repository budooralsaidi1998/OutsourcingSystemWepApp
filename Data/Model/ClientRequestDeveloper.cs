using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class ClientRequestDeveloper
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Required]
        [ForeignKey(nameof(Developer))]
        public int DeveloperID { get; set; }
        public Developer Developer { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
    }
}
