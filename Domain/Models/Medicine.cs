using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Medicine
    {
        [Required]
        public string? MedicineName { get; set; }
        [Required]
        public decimal MedicineRate { get; set; }
        [Required]
        public int MedicineQuantity { get; set; }
        [Required]
        public ScheduleTime MedicineScheduledTime { get; set; }
        public decimal TotalCost => MedicineQuantity * MedicineRate;
    }
   
}
