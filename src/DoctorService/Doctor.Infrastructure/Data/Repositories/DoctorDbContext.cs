using Microsoft.EntityFrameworkCore;
using DoctorService.Domain.Entities;

namespace DoctorService.Infrastructure.Data.Repositories
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<AvailabilitySchedule> Schedules => Set<AvailabilitySchedule>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctors");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id).HasColumnName("id");
                entity.Property(d => d.UserId).HasColumnName("user_id");
                entity.Property(d => d.Name).HasColumnName("Name").IsRequired().HasMaxLength(255);
                entity.Property(d => d.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(d => d.Specialty).HasColumnName("specialty").IsRequired().HasMaxLength(100);
                entity.Property(d => d.LicenseNumber).HasColumnName("license_number").IsRequired().HasMaxLength(50);
                entity.Property(d => d.PhotoUrl).HasColumnName("photo_url");
                entity.Property(d => d.Bio).HasColumnName("bio");
                entity.Property(d => d.IsActive).HasColumnName("is_active");
                entity.Property(d => d.CreatedAt).HasColumnName("created_at");

                // Un médico no puede registrarse dos veces
                entity.HasIndex(d => d.UserId).IsUnique();
                entity.HasIndex(d => d.Email).IsUnique();

                // Relación Doctor → Schedules
                entity.HasMany(d => d.Schedules)
                      .WithOne()
                      .HasForeignKey(s => s.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<AvailabilitySchedule>(entity =>
            {
                entity.ToTable("availability_schedules");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id).HasColumnName("id");
                entity.Property(s => s.DoctorId).HasColumnName("doctor_id");
                entity.Property(s => s.DayOfWeek).HasColumnName("day_of_week");
                entity.Property(s => s.StartTime).HasColumnName("start_time");
                entity.Property(s => s.EndTime).HasColumnName("end_time");
                entity.Property(s => s.IsActive).HasColumnName("is_active");
            });

        }

    }
}
