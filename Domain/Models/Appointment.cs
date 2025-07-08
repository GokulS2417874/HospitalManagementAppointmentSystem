using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsFollowUpRequired { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string FilePath { get; set; }

        //One to Many Realationship (Child class)

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        //One to Many Realationship (Child class)
        public int DoctorId { get; set; }
        public Doctor Doctor {  get; set; }
        public Prescription Prescription { get; set; }

        public int? PaymentId { get; set; }
        public Payment Payment{ get; set; }

        [NotMapped]
        public ICollection<Notification> Notifications { get; set; }

    }
}
