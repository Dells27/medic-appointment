using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientService.Application.Interfaces;
using PatientService.Infrastructure.Data;
using PatientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PatientService.Infrastructure.Data
{
    public class PatientRepository : IPatientRepository
    {

        private readonly PatientDbContext _context;
      public PatientRepository(PatientDbContext context) { 
        _context = context;
       
        }


        public async Task<Patient?> GetByIdAsync (Guid id)
        {
            return await _context.Patients
                .Include(p => p.Document)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<Patient?> GetByUserId(Guid userId)
        {
            return await _context.Patients
                .Include(p => p.Document)
                .FirstOrDefaultAsync (p => p.userId == userId);
        }

        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await _context.Patients.AnyAsync(p => p.userId == userId);
        }

        public async Task AddAsync (Patient entity)
        {
            await _context.Patients.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
