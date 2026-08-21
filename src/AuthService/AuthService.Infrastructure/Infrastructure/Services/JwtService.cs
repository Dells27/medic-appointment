using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace AuthService.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private const int ExpirationHours = 24;

        public JwtService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
 

        public string GenerateToken (User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("name", user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!
                ));


            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: GetExpirationDate(),
                signingCredentials: credentials
                
                );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }

        public DateTime GetExpirationDate()
            { return DateTime.UtcNow.AddHours(ExpirationHours); }

    }


}
