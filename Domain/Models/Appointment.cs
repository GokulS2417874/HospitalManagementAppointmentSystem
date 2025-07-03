using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Appointment 
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }= DateTime.Now;
        public AppointmentStatus AppointmentStatus { get; set; }
        public AppointmentType AppointmentBookedBy { get; set; }
        //public string? PrescriptionFromDoctor { get; set; }
        //public string? PatientMedicalHistroy { get; set; }
        public bool IsFollowUpRequired { get; set; }
        //public string? Diagnosis { get; set; }
        public DateTime? FollowUpDate { get; set; }

        //One to Many Realationship (Child class)
        public Patient Patient { get; set; }    
        public Doctor Doctor {  get; set; }
        public MedicalHistory MedicalHistory { get; set; }
    }
}
