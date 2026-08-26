using DoctorService.Application.DTOs;
using DoctorService.Application.Interfaces;
using DoctorService.Application.UseCases.CreateDoctor;

namespace DoctorService.Application.UseCases.GetDoctor
{
    public class GetDoctorHandler
    {
        private readonly IDoctorRepository _repository;

        public GetDoctorHandler(IDoctorRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los médicos
        public async Task<List<DoctorResponse>> HandleGetAll()
        {
            var doctors = await _repository.GetAllAsync();
            return doctors.Select(d => CreateDoctorHanlder.MapToResponse(d)).ToList();
        }

        // Obtener médico por ID
        public async Task<DoctorResponse> HandleGetById(Guid id)
        {
            var doctor = await _repository.GetByIdAsync(id);
            if (doctor is null)
                throw new Exception("Médico no encontrado");

            return CreateDoctorHanlder.MapToResponse(doctor);
        }

        // Obtener médicos por especialidad
        public async Task<List<DoctorResponse>> HandleGetBySpecialty(string specialty)
        {
            var doctors = await _repository.GetBySpecialtyAsync(specialty);
            return doctors.Select(CreateDoctorHanlder.MapToResponse).ToList();
        }

    }
}
