using System.ComponentModel.DataAnnotations;

namespace OutsourcingSystemWepApp.Data.Model
{
    public class User
    {
        [Key]
        public int UID { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Nmae cannot more than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one letter and one number.")]
        public string Password { get; set; }




        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(Developer|Admin|Client)$", ErrorMessage = "The role must be one of the following: Developer, Admin, Client.")]
        public string role { get; set; }   //"client " , "Developer" , "Admin"


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
