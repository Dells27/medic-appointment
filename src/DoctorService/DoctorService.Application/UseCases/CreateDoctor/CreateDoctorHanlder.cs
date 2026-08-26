using DoctorService.Domain.Entities;
using DoctorService.Application.DTOs;
using DoctorService.Application.Interfaces;


namespace DoctorService.Application.UseCases.CreateDoctor
{
    public class CreateDoctorHanlder
    {
        private readonly IDoctorRepository _repository;
        public CreateDoctorHanlder(IDoctorRepository repository)
        {
           _repository = repository;
        }

        public async Task<DoctorResponse> Handle(CreateDoctorRequest request)
        {
            // Verificar que no exista ya un perfil para este usuario
            var exists = await _repository.ExistsAsync(request.userId);
            if (exists)
                throw new Exception("Este usuario ya tiene un perfil de médico");

            var doctor = Doctor.Create(
                request.userId,
                request.name,
                request.Email,
                request.Specialty,
                request.LicenseNumber
                );


            await _repository.AddAsync( doctor );
            await _repository.SaveChangesAsync();
            return MapToResponse( doctor );
        }

        public static DoctorResponse MapToResponse(Doctor doctor)
        {
            return new DoctorResponse
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                Name = doctor.Name,
                Email = doctor.Email,
                Specialty = doctor.Specialty,
                LicenseNumber = doctor.LicenseNumber,
                PhotoUrl= doctor.PhotoUrl,
                Bio= doctor.Bio,
                Schedules=doctor.Schedules.Select(s=>new ScheduleResponse
                {
                    Id =s.Id,
                    DayOfWeek = s.DayOfWeek,
                    StartTime = s.StartTime,
                    EndTime=s.EndTime,
                }).ToList(),

            };
        }

    }
}
