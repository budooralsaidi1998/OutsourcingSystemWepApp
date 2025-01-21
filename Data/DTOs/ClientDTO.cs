namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class ClientDTO
    {
        public int ClientID { get; set; }
        public int userid { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public decimal? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
