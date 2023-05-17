
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ItechCleanArst.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}