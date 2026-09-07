using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientService.Domain.Entities;

namespace PatientService.Application.Interfaces
{
    public interface IPatientRepository
    {
         Task<Patient?> GetByIdAsync(Guid id);
         Task<Patient?> GetByUserId(Guid userId);
         Task<bool> ExistsAsync(Guid userDd);
        Task AddAsync(Patient entity);
        Task SaveChangesAsync();
    }
}
