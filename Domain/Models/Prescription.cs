using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int PatientId { get; set; }
        public Users Patient { get; set; }
        public int DoctorId { get; set; }
        public Users Doctor { get; set; }
        public MedicineType Medication { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public TabletScheduleTime ScheduleTime { get; set; }
        public DateTime PrescribedOn { get; set; } = DateTime.Now;
    }
}
