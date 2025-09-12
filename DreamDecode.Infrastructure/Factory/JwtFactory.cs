using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Domain.User.Entities;
using DreamDecode.Domain.User.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DreamDecode.Infrastructure.Factory
{
    public class JwtTokenFactory : IJwtFactory
    {
        private readonly IConfiguration _cfg;
        public JwtTokenFactory(IConfiguration cfg) => _cfg = cfg;

        public string Create(ApplicationUser user, string role)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Role, role)
            };

            Console.WriteLine("Claims created:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"  {claim.Type}: {claim.Value}");
            }

            var keyString = _cfg["Jwt:Key"];
            var issuer = _cfg["Jwt:Issuer"];
            var audience = _cfg["Jwt:Audience"];


            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key is not configured");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}