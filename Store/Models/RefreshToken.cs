namespace Store.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime RefreshTokenCreationDate { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
