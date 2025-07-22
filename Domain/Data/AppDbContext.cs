using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
        {
        }
        public DbSet<Users>  Users{ get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany( u => u.Payments)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
               .HasOne(a => a.Appointment)
               .WithMany()
               .HasForeignKey(a => a.AppointmentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p=> p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Prescription>()
            //    .Property(p => p.Medicines)
            //    .HasConversion<int>();

        }
    }
}
