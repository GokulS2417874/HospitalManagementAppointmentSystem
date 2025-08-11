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
        public int DoctorId { get; set; }
        public Users Patient { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PendingAmount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentMethod PaymentMethod { get; set; }
    }
}
