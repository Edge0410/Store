namespace Store.Models.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public UserResponseDto(User user, string token, RefreshToken refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
