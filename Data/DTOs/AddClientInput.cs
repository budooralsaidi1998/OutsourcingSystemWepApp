namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class AddClientInput
    {
        public int userid { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public decimal? Rating { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
