using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly AuthDbContext _context;


        public UserRepository(AuthDbContext context)
        {
        
        _context = context;

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email.ToLower().Trim());
        }

        public async Task AddAsync(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

    }
}
