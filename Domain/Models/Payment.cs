using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int PatientId { get; set; }
        public Users Patient { get; set; }


        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public decimal Amount { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
