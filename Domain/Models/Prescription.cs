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
        public int PrescriptionId { get; set; }
        public string MedicineName { get; set; }
        public ShiftTime? MedicineScheduledTime { get; set; }
    }
}
