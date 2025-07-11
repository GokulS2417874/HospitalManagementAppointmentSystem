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
        public DbSet<Users>  Users{ get; set; }
        //public DbSet<Admin> Admins { get; set; }
        //public DbSet<Patient> Patients {  get; set; }   
        //public DbSet<Doctor> Doctors { get; set; }
        //public DbSet<HelpDesk> HelpDesks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
