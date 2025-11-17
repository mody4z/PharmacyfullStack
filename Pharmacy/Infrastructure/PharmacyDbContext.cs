using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Infrastructure
{
    public class PharmacyDbContext : IdentityDbContext<ApplicationUser>
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<InOut> InOuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision
            modelBuilder.Entity<Medicine>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            // Configure relationships
            modelBuilder.Entity<InOut>()
                .HasOne(io => io.Medicine)
                .WithMany()
                .HasForeignKey(io => io.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InOut>()
                .HasOne(io => io.Employee)
                .WithMany()
                .HasForeignKey(io => io.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InOut>()
                .HasOne(io => io.Client)
                .WithMany()
                .HasForeignKey(io => io.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure ApplicationUser - Employee relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
