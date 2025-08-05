using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Payment → Patient (User)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment → Appointment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prescription → Patient (User)
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: Configure PrescriptionMedicine relationships if needed
            // modelBuilder.Entity<PrescriptionMedicine>()
            //     .HasOne(pm => pm.Prescription)
            //     .WithMany(p => p.Medicines)
            //     .HasForeignKey(pm => pm.PrescriptionId);
        }
    }
}
