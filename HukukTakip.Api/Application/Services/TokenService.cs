using HukukTakip.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HukukTakip.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _cfg;
        public TokenService(IConfiguration cfg) => _cfg = cfg;

        public (string token, DateTime expiresAt) CreateToken(User user)
        {
            var jwt = _cfg.GetSection("Jwt");
            var issuer = jwt["Issuer"];          // "HukukTakipApi"
            var audience = jwt["Audience"];        // "HukukTakipClient"
            var key = jwt["Key"]!;
            var minutes = int.Parse(jwt["ExpireMinutes"]!);

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(minutes);

            var role = (user.Rol ?? "user").Trim().ToLowerInvariant();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.KullaniciAdi ?? user.Email),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, role)
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
