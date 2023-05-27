
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ItechCleanArst.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}