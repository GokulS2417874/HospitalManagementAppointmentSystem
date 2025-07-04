using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
        {
        }

        public DbSet<Patient> Patients {  get; set; }   
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Payment> Payments {  get; set; }
        public DbSet<Notification> Notifications { get; set; } 
        public DbSet<HelpDesk> HelpDesks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent Api
            // One-to-Many: Patient -> Appointments
            modelBuilder.Entity<Patient>()
                        .HasMany(p => p.Appointment)
                        .WithOne(a => a.Patient)
                        .HasForeignKey(a => a.PatientId);

            // One-to-Many: Doctor -> Appointments
            modelBuilder.Entity<Doctor>()
                        .HasMany(d => d.Appointment)
                        .WithOne(a => a.Doctor)
                        .HasForeignKey(a => a.DoctorId);

            // One-to-One: Appointment -> Prescription
            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Prescription)
                        .WithOne(p => p.Appointment)
                        .HasForeignKey<Prescription>(p => p.AppointmentId);

            //One-to-One: Appointment -> Payment
            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Payment)
                        .WithOne(p => p.Appointment)
                        .HasForeignKey<Payment>(p => p.AppointmentId);

            // One-to-Many: HelpDesk -> Patients
            modelBuilder.Entity<Patient>()
                        .HasOne(p => p.HelpDesk)
                        .WithMany(h => h.Patients)
                        .HasForeignKey(p => p.HelpDeskId);

            // One-to-Many: Appointment -> Notifications
            modelBuilder.Entity<Appointment>()
                        .HasMany(a => a.Notifications)
                        .WithOne(n => n.Appointment)
                        .HasForeignKey(n => n.AppointmentId);

            base.OnModelCreating(modelBuilder); 
        }

    }
}
