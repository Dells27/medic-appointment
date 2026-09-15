using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentService.Application.DTOs;
using AppointmentService.Application.Interfaces;
using AppointmentService.Application.UseCases.CreateAppointment;

namespace AppointmentService.Application.UsesCases.GetAppointment;

public class GetAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentService (IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentResponse>> HandleByPatient (Guid patientId)
    {
        var appointments = await _appointmentRepository.GetByPatientIdAsync (patientId);
        return appointments.Select(CreateAppointmentHandler.MapToResponse).ToList();
    }

    public async Task<List<AppointmentResponse>> HandleByDoctor (Guid doctorId)
    {
        var appointments = await _appointmentRepository.GetByDoctorIdAsync (doctorId);
        return appointments.Select(CreateAppointmentHandler.MapToResponse).ToList ();
    }

    public async Task<AppointmentResponse> HandleById(Guid id)
    {
        var appointments = await _appointmentRepository.GetByIdAsync (id);
        if (appointments is null)
            throw new Exception("Cita no encontrada");
        return CreateAppointmentHandler.MapToResponse(appointments);
    }

}
