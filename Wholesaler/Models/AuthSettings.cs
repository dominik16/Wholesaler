namespace Wholesaler.DataTransferObject
{
    public class AuthSettings
    {
        public string JwtKey { get; set; } = string.Empty;
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; } = string.Empty;
    }
}
