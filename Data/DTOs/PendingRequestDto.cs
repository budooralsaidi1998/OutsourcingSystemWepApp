namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class PendingRequestDto
    {
        public int RequestId { get; set; }
        public string Type { get; set; }
        public int ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string ClientName {  get; set; }

    }
}
