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
        public DateOnly AppointmentDate { get; set; } 
        public TimeOnly? AppointmentStartTime { get; set; }
        public TimeOnly? AppointmentEndTime { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; } = Enum.AppointmentStatus.InProgress;
        //public bool? IsFollowUpRequired { get; set; }
        //public DateTime? FollowUpDate { get; set; }
        //public string FilePath { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public specialization Specialization { get; set; }


    }
}
