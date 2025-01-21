namespace OutsourcingSystemWepApp.Data.DTOs
{
    public class ProcessRequestDto
    {
        public int RequestId { get; set; } // The ID of the request to process
        public bool IsAccepted { get; set; } // Whether the request is accepted or rejected
        public string RequestType { get; set; } // Developer or Team
    }
}
