
namespace DoctorService.Domain.Entities;

public class Doctor
{

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Specialty { get; private set; }
    public string LicenseNumber { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string? Bio { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<AvailabilitySchedule> _schedules = new();
    public IReadOnlyCollection<AvailabilitySchedule> Schedules => _schedules.AsReadOnly();

    private Doctor() { }

    public static Doctor Create(Guid userId, string name, string email, string specialty, string licenseNumber)
    {
        return new Doctor {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name,
            Email = email,
            Specialty = specialty,
            LicenseNumber = licenseNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,

        };
        
    }


    public void UpdateProfile (string name, string specialty, string? bio, string? photoURL)
    {
        Name = name;
        Specialty = specialty;
        Bio = bio;
        PhotoUrl = photoURL;
    }
}

