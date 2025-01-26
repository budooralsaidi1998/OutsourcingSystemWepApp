namespace OutsourcingSystemWepApp.helpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = "YourSuperSecretKeyForJWTGeneration";
        public int ExpirationInMinutes { get; set; } = 60;
    }
}
