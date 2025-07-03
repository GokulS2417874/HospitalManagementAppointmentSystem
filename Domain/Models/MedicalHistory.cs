using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MedicalHistory
    {
        [Key]
        public int ReportId { get; set; }
        public int AppointmentId { get; set; }
        public int PrescriptionID { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateOfTreatment { get; set; }
        public string? Notes { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

    }
}
