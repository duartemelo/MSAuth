using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSAuth.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserClaims user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            SigningCredentials signingCredential = new(
            securityKey,
            SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(Double.Parse(_configuration.GetSection("Jwt:ExpiresMin").Value!)),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredential);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
