using Domain.Models;
using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class PrescriptionDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Instructions { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; }    
    }

    public class PrescriptionMedicineDto
    {
        public string MedicineType { get; set; }
        public string Dosages { get; set; }
        public string ScheduleTime { get; set; }

    }
}
