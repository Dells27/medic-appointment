using AppointmentService.Application.DTOs;
using AppointmentService.Application.Interfaces;
using AppointmentService.Application.UseCases.CreateAppointment;
using AppointmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentService.Application.UsesCases.CancelAppointment;

public class CancelAppointmentHandler
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ICacheService _cacheService;
    private readonly IEventPublisher _eventPublisher;

    public CancelAppointmentHandler (IAppointmentRepository appointmentRepository, ICacheService cacheService, IEventPublisher eventPublisher)
    {
        _appointmentRepository = appointmentRepository;
        _cacheService = cacheService;
        _eventPublisher = eventPublisher;
    }


    public async Task<AppointmentResponse> Handle (Guid appoinmentId, string reason)
    {
        var appointment = await _appointmentRepository.GetByIdAsync (appoinmentId);
        if (appointment == null)
            throw new Exception("Cita no encontrada");

        appointment.Cancel(reason);
        await _appointmentRepository.SaveChangesAsync();

        var cacheKey = $"slot:{appointment.doctorId}:{appointment.appointmentDate:yyyy-MM-dd}:{appointment.appointmentTime}";
        await _cacheService.RemoveAsync (cacheKey);


        await _eventPublisher.PublishAsync("appointment.cancelled", new AppointmentCancelledEvent
        {
            appointmentId = appoinmentId,
            patientId=appointment.patientId,
            doctorId=appointment.doctorId,
            reason = reason

        });

        return CreateAppointmentHandler.MapToResponse (appointment);
    }
}
