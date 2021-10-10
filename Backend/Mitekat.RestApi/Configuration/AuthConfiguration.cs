namespace Mitekat.RestApi.Configuration
{
    public class AuthConfiguration
    {
        public string SecretKey { get; set; }
        public int AccessTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenLifetimeInDays { get; set; }
    }
}
