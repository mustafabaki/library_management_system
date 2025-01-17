using library_management_system.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace library_management_system.Utils
{
    public class Utilities
    {
        public static Member HashPassword(Member member)
        {
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            return member;
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
            
        }

        public static bool VerifyPassword(string password, Member member)
        {
            return BCrypt.Net.BCrypt.Verify(password, member.Password);
        }

        public static string GenerateToken(IConfiguration _config, Guid userId)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("userId", userId.ToString()),

        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(jwtSettings["DurationInHours"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
