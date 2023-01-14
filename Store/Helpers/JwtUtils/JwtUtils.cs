using Store.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Azure;

namespace Store.Helpers.JwtUtils
{
    public class JwtUtils: IJwtUtils
    {
        public readonly AppSettings _appSettings;
        private readonly IConfiguration _config;

        public JwtUtils(IOptions<AppSettings> appSettings, IConfiguration config)
        {
            _appSettings = appSettings.Value;
            _config = config;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                 Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                 RefreshTokenExpirationDate = DateTime.Now.AddDays(1),
                 RefreshTokenCreationDate = DateTime.Now
            };

            return refreshToken;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:JwtToken"));

            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(appPrivateKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);

            return tokenHandler.WriteToken(token);
        }

        public Guid ValidateJwtToken(string token)
        {
            if(token == null)
            {
                return Guid.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:JwtToken"));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(appPrivateKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = new Guid(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);
                return userId;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
