using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class AppointmentDto
    {
        //public int PatientId { get; set; }
        //public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        //public string Specialization { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly? AppointmentStartTime { get; set; }
        public TimeOnly? AppointmentEndTime { get; set; }
        public bool Submit { get; set; } = false;

    }
    public class DoctorAppointmentUpdateDto
    {
        public bool IsAttended { get; set; }
    }
}
