using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class PrescriptionMedicine
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public string MedicineType { get; set; }
        public string Dosages { get; set; }
        public Prescription Prescription { get; set; }
        public string ScheduleTime { get; set; }
    }

}
