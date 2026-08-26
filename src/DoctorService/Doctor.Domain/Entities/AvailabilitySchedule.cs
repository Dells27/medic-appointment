

namespace Doctor.Domain.Entities
{
    public class AvailabilitySchedule
    {
        public Guid Id { get; private set; }
        public Guid DoctorId { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set;}
        public bool IsActive { get; private set; }


        private AvailabilitySchedule() { }

        public static AvailabilitySchedule Create(Guid doctorId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
        { 
            if (startTime >= endTime)
                throw new Exception("La hora de inicio debe ser menor a la hora de fin");
            


            return new AvailabilitySchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctorId,
                DayOfWeek = dayOfWeek,
                StartTime = startTime,
                EndTime = endTime,
                IsActive = true


            };
        }


        public void Deactivate () => IsActive = false;

    }
}
