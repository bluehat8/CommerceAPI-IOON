using CommerceAPI_IOON.CommerceAPI.Models;
using CommerceAPI_IOON.CommerceInfrastructure.Context.CredipathAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommerceAPI_IOON.CommerceAPI.Services
{
    public class CommerceService
    {
        private readonly DataContext _context;

        public CommerceService(DataContext context)
        {
            _context = context;
        }

        public async Task RegisterCommerceAndUserAsync(string commerceName, string commerceAddress, string ruc, string userName, string userEmail, string userPassword)
        {
            var activeState = await _context.States.FirstOrDefaultAsync(s => s.StateName == "Active");

            if (activeState == null)
            {
                throw new Exception("Estado 'Active' no encontrado en la base de datos.");
            }


            var commerce = new Commerce
            {
                CommerceId = Guid.NewGuid(),
                CommerceName = commerceName,
                RUC = ruc,
                Address = commerceAddress,
                State = activeState
            };

            _context.Commerces.Add(commerce);
            await _context.SaveChangesAsync(); 

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userName,
                Password = userPassword,
                Role = "Owner",
                State = activeState,
                CommerceId = commerce.CommerceId
            };

            var passwordHasher = new PasswordHasher<User>();

            user.Password = passwordHasher.HashPassword(user, user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 
        }



        public async Task DeleteUserAndCommerceAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Commerce)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.Role == "Owner")
            {
                var commerce = user.Commerce;
                if (commerce != null)
                {
                    _context.Commerces.Remove(commerce);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

    }
}
