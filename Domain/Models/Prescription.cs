using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Prescription : Medicine
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        public long AppointmentId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string? Notes { get; set; }
    }
}
