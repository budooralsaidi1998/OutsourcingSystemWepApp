namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class RequestDto
    {
        public string RequestType { get; set; } // Developer or Team
                                                // public int? ClientID { get; set; } // Optional; filled from JWT Token
        public int? developerid { get; set; } // Only for Developer requests
        public int? TeamID { get; set; } // Only for Team requests
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
