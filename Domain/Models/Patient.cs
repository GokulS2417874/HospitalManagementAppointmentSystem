using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Patient : Users
    {
        public Patient()
        {
            Role = UserRole.Patient;
        }
        [Required]
        public DateTime DateOfBrith { get; set; }
        [Required]
        public PatientGender Gender { get; set; }
        public AppointmentType AppointmentBookedBy { get; set; }
        public HelpDesk HelpDesk { get; set; }  

        //One to Many Realationship (Parent class)
        public IEnumerable<Appointment> Appointment { get; set; }  
    }
}


   