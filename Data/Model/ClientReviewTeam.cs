using static MudBlazor.Colors;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class ClientReviewTeam
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }


        [Required]
        [ForeignKey(nameof(Teams))]
        public int TeamID { get; set; }
        public  Team Teams { get; set; }


        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
