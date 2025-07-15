using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class PrescriptionDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public List<MedicineType> Medication { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public TabletScheduleTime ScheduleTime { get; set; }
    }

}
