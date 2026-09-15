using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentService.Domain.Entities;
using AppointmentService.Application.Interfaces;
using AppointmentService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Infrastructure.Data.Repositories;

public class AppointmentRepository: IAppointmentRepository
{
    private readonly AppointmentDbContext _context;

    public AppointmentRepository(AppointmentDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {

        return await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Appointment>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Appointments
            .Where(a => a.patientId == patientId)
            .OrderByDescending(a => a.appointmentDate)
            .ToListAsync();
    }


    public async Task<List<Appointment>> GetByDoctorIdAsync (Guid doctorId)
    {
        return await _context.Appointments.
            Where(x => x.doctorId == doctorId).
            OrderByDescending(x => x.appointmentDate).
            ToListAsync();
    }


    public async Task<List<Appointment>> GetByDoctorAndDateAsync (Guid doctorId, DateTime date)
    {
        return await _context.Appointments.Where(x=>x.doctorId == doctorId
        && x.appointmentDate == date 
        && x.status != AppointmentStatus.Cancelled)
        .ToListAsync();
    }

    public async Task<bool> IsSlotTakenAsync(Guid doctorId, DateTime date, TimeOnly time)
    {
        return await _context.Appointments.AnyAsync(
            x => x.doctorId == doctorId
            && x.appointmentDate == date
            && x.appointmentTime == time);
    }

    public async Task AddAsync (Appointment appointment)
    {
         await _context.AddAsync(appointment);
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
