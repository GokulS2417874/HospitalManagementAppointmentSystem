using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int TotalAmount { get; set; }
        public Appointment Appointment { get; set; }
        public Appointment AppointmentId { get; set; }
    }
}
