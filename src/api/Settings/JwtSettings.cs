namespace API.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int TokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }
}