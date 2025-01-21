namespace OutsourcingSystemWepApp.Configurations
{
    public class EmailSettings
    {
        public string SMTPHost { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string AdminEmail { get; set; }
    }
}
