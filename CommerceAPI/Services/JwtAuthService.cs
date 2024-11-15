using CommerceAPI_IOON.CommerceAPI.Models;
using CommerceAPI_IOON.CommerceInfrastructure.Context.CredipathAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommerceAPI_IOON.CommerceAPI.Services
{
    public class JwtAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public JwtAuthService(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public bool VerifyPassword(User user, string password)
        {
           
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Failed;
        }

        public async Task<string> GenerateJwtTokenAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.State)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado.");
            }

            if (user.State == null || user.State.StateName != "Active")
            {
                throw new UnauthorizedAccessException("El estado del usuario no es activo.");
            }

            var validPassword = VerifyPassword(user, password);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.UserId.ToString()),
                new Claim("stateId", user.StateId.ToString()), 
                new Claim("role", user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
