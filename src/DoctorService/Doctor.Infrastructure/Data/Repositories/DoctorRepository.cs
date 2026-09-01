using DoctorService.Application.Interfaces;
using DoctorService.Domain.Entities;
using DoctorService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Infrastructure.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {

        private readonly DoctorDbContext _context;

        public DoctorRepository(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetByIdAsync (Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Schedules)
                .FirstOrDefaultAsync (d => d.Id == id);
        }

        public async Task<Doctor?> GetByUserIdAsync (Guid userId)
        {
            return await _context.Doctors
                .Include(d => d.Schedules)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.Schedules)
                .Where(d => d.IsActive)
                .ToListAsync();
        }


        public async Task<List<Doctor>> GetBySpecialtyAsync (string specialty)
        {
            return await _context.Doctors
                .Include(d => d.Schedules)
                .Where(d => d.Specialty.ToLower() == specialty.ToLower()&& 
                d.IsActive)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync (Guid userId)
        {
            return await _context.Doctors
                .AnyAsync(d => d.UserId == userId && d.IsActive); ;
        }
        
        public async Task AddAsync (Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
