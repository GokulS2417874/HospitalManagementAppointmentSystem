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
        public byte[]? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? MimeType { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public specialization Specialization { get; set; }
        public bool IsReminderSent { get; set; }

    }
}
