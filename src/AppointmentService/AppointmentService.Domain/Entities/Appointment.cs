using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentService.Domain.Entities
{
    public enum AppointmentStatus
    {
        Scheduled,
        Confirmed,
        Completed,
        Cancelled,
        NoShow
    }


    public class Appointment
    {
       
        public Guid Id { get; private set; }
        public Guid patientId { get; private set; }
        public Guid doctorId { get; private set; }
        public DateTime appointmentDate { get; private set; }
        public TimeOnly appointmentTime { get; private set; }
        public AppointmentStatus status { get; private set; }
        public string? reason { get; private set; }
        public string? cancellationReason { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime? updatedAt { get; private set; }


        private Appointment() { }

        public static Appointment Create (Guid PatientId, Guid DoctorId, DateTime AppointmentDate, TimeOnly AppointmentTime, string? Reason)
        {
            return new Appointment {
                Id = Guid.NewGuid(),
                patientId = PatientId,
                doctorId = DoctorId,
                appointmentDate = AppointmentDate,
                appointmentTime = AppointmentTime,
                reason = Reason,
                createdAt = DateTime.UtcNow
            };
        }


        public void Confirm()
        {
            if (status != AppointmentStatus.Scheduled)
                throw new Exception("Solo se pueden confirmar citas agendadas");

            status = AppointmentStatus.Confirmed;
            updatedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (status != AppointmentStatus.Confirmed && status != AppointmentStatus.Scheduled)
                throw new Exception("No se puedecompletar esta cita");
            status= AppointmentStatus.Completed;
            updatedAt = DateTime.UtcNow;
        }

        public void Cancel (string reason)
        {
            if (status == AppointmentStatus.Completed)
                throw new Exception("No se puede cancelar una cita completada");
            status= AppointmentStatus.Cancelled;
            cancellationReason = reason;
            updatedAt= DateTime.UtcNow;
        }

        public void MarkAsNoShow()
        {
            status = AppointmentStatus.NoShow;
            updatedAt = DateTime.UtcNow;
        }


    }
}
