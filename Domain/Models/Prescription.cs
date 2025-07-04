using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        public string MedicineName { get; set; }
        public ScheduleTime MedicineScheduledTime { get; set; }
        public Appointment Appointment{ get; set; }
        public Appointment AppointmentId{ get; set; }
    }
}
