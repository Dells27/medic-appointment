

namespace DoctorService.Application.DTOs;

//Crear perfil médico
public class CreateDoctorRequest
{
    public Guid userId { get; set; }
    public string name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialty {  get; set; } = string.Empty;
    public string LicenseNumber {  get; set; } = string.Empty;
}

//Actualizar Perfil

public class UpdateDoctorRequest
{
    public string name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Bio {  get; set; }
    public string? PhotoUrl { get; set; }
}

// Agregar Horario
public class AddScheduleRequest
{
    public DayOfWeek DayOfWeek {  get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

public class DoctorResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string? PhotoUrl {get; set;}
    public string? Bio { get; set; }
    public List<ScheduleResponse> Schedules { get; set; } = new();
}


public class ScheduleResponse
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
