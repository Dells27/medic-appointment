using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AuthService.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }


        public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.ID);

                entity.ToTable("users");

                entity.Property(u => u.ID)
                .HasColumnName("id");

                entity.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(250);

                entity.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

                entity.Property(u => u.Role)
                .HasColumnName("role")
                .IsRequired()
                .HasMaxLength (250);

                entity.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(250);

                entity.Property(u => u.CreatedAt)
                .HasColumnName("created_at");

                entity.Property(u => u.IsActive)
                .HasColumnName("is_active");

                entity.HasIndex(u => u.Email)
                .IsUnique();

            });

        }
    }
}
