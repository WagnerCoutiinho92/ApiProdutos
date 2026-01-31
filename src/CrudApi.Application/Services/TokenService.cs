using CrudApi.Application.Interfaces.Services;
using CrudApi.Application.Settings;
using CrudApi.Domain.Entities;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace CrudApi.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _settings;

        public TokenService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(
            string userId,
            string email,
            string nome,
            IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Email, email),
                new("nome", nome)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_settings.ExpirationHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
