using Microsoft.IdentityModel.Tokens;
using PetitionBackend.Models;
using PetitionBackend.DbContexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetitionBackend.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public TokenService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public (string AccessToken, string RefreshToken) GenerateTokens(User user)
        {
            var AccessToken = GenerateAccessToken(user);
            var RefreshToken = GenerateRefreshToken();
            SaveRefreshToken(user, RefreshToken);
            return (AccessToken, RefreshToken);
        }

        public RefreshToken? GetRefreshToken(string RefreshToken)
        {
            return _context.RefreshTokens.FirstOrDefault(
                token => token.Token == RefreshToken
            );
        }

        public void SaveRefreshToken(User user, string RefreshToken)
        {
            var token = _context.RefreshTokens.FirstOrDefault(
                token => token.UserId == user.Id
            );

            if (token != null) _context.RefreshTokens.Remove(token);

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_configuration["TokensLifetime:RefreshToken"]))
            });
            _context.SaveChanges();
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_configuration["TokensLifetime:AccessToken"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
